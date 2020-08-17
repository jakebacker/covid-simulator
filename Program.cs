using System;
using System.Collections;

/*
 * TODO:
 * Restructure all the code. Make it not jank
 * Visualize the data much better, allowing for more data
 * Investigate https://rt.live/ more
 * Input people in a better way
 * Generate random people arrangements
 * Consider models other than SIR
 * Symptom tracking
 */

namespace CovidSimulator
{
    // TODO: Rewrite all of this. Break it out into more methods. Make it more modular.
    class Program
    {

        public static int TotalInfected = 0;

        public static int Infected = 0;

        public static readonly Random Rand = new Random();

        private const bool Debug = false;

        private const int NumSims = 10000;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Simulation...");

            double simSum = 0;

            double infectedSum = 0;

            ArrayList people = new ArrayList();

            for (int s = 0; s < NumSims; s++)
            {
                Program.DebugPrint("Simulation " + s);

                Infected = 0;

                TotalInfected = 0;

                people = new ArrayList();

                Person person0 = new Person("Person0");
                Person person1 = new Person("Person1");
                Person person2 = new Person("Person2");
                Person person3 = new Person("Person3");
                Person person4 = new Person("Person4");
                Person[] residence1 = {person0, person1, person2, person3, person4};

                Person person5 = new Person("Person5");
                Person person6 = new Person("Person6");
                Person person7 = new Person("Person7");
                Person person8 = new Person("Person8");
                Person person9 = new Person("Person9");
                Person[] residence2 = {person5, person6, person7, person8, person9};

                Person person10 = new Person("Person10");
                Person person11 = new Person("Person11");
                Person person12 = new Person("Person12");
                Person person13 = new Person("Person13");
                Person person14 = new Person("Person14");
                Person[] residence3 = {person10, person11, person12, person13, person14};

                Person person15 = new Person("Person15");
                Person person16 = new Person("Person16");
                Person person17 = new Person("Person17");
                Person[] residence4 = {person15, person16, person17};

                Person person18 = new Person("Person18");

                Person[] friendGroup =
                {
                    person0, person5, person6, person7, person8, person9, person10, person11, person12, person13, person14, person15, person16,
                    person17, person18
                }; 

                ResidenceConnections(residence1);
                ResidenceConnections(residence2);
                ResidenceConnections(residence3);
                ResidenceConnections(residence4);

                person0.AddConnection(person5, ConnectionType.Medium);
                person5.AddConnection(person0, ConnectionType.Medium);

                person6.AddConnection(person10, ConnectionType.Medium);
                person10.AddConnection(person6, ConnectionType.Medium);

                person14.AddConnection(person15, ConnectionType.Medium);
                person15.AddConnection(person14, ConnectionType.Medium);

                person16.AddConnection(person18, ConnectionType.Medium);
                person18.AddConnection(person16, ConnectionType.Medium);
                
               //FriendConnections(friendGroup); // This needs to be last. Pretty much make closer connections first.
                
                people.Add(person0);
                people.Add(person1);
                people.Add(person2);
                people.Add(person3);
                people.Add(person4);

                people.Add(person5);
                people.Add(person6);
                people.Add(person7);
                people.Add(person8);
                people.Add(person9);

                people.Add(person10);
                people.Add(person11);
                people.Add(person12);
                people.Add(person13);
                people.Add(person14);

                people.Add(person15);
                people.Add(person16);
                people.Add(person17);

                people.Add(person18);

                int randPerson = Rand.Next(0, people.Count);

                Person infectedPerson = (Person)people[randPerson];
                
                infectedPerson.Infect();

                int peakInfections = 1;

                int simDay = 1;
                bool activeInfections = true;
                while (activeInfections)
                {
                    
                    Program.DebugPrint("\nDay " + simDay + " has begun");
                    Program.DebugPrint("There are " + Infected + " active infections.");

                    // Everyone gets tested
                    foreach (Person person in people)
                    {
                        if (person.WillQuarantine()) // 1 day turn around for testing
                        {
                            // Remove them. Get rid of connections first
                            Program.DebugPrint(person.GetName() + " was Quarantined");

                            ArrayList connections = person.GetConnections();

                            person.RemoveConnections();

                            // Also quarantine their roommates and partners
                            foreach (Connection c in connections)
                            {
                                c.GetOtherPerson().RemoveConnections();
                            }
                        }

                        if ((simDay + person.GetTestDay()) % 7 == 0) { // This causes staggered test days
                            if (person.IsInfected())
                            {
                                double num = Rand.NextDouble();

                                // False negative
                                if (num <= CovidStatsConfig.FalseNegativeRates[person.GetLastUpdate()])
                                {
                                    Program.DebugPrint(person.GetName() + " got a false negative");
                                    continue;
                                }
                                
                                person.Quarantine(); // 1 day turn around
                            }
                        }
                    }

                    foreach (Person person in people)
                    {
                        if (!person.IsInfected()) continue;

                        person.UpdateDay();
                        if (person.GetLastUpdate() >= CovidStatsConfig.AverageLength)
                        {
                            person.Recover();
                        }
                        else
                        {
                            person.UpdateConnections();
                        }
                    }

                    if (Infected > peakInfections) peakInfections = Infected;

                    if (Infected <= 0)
                    {
                        Program.DebugPrint("\nNo more active infections. Simulation complete\n");
                        simSum += simDay;
                        activeInfections = false;
                    }

                    simDay++;
                }

                infectedSum += TotalInfected;

                Program.DebugPrint("There were " + peakInfections + " at it's peak.");
                Program.DebugPrint("In total, " + TotalInfected + " people wre infected");

                Program.DebugPrint("-------------------------------------------------------------------------------");
            }

            Console.WriteLine("Average Sim Length: " + simSum / NumSims);
            Console.WriteLine("Average Total Infected: " + infectedSum / NumSims + "/" + people.Count);
        }

        private static void ResidenceConnections(Person[] residence)
        {
            foreach (Person p1 in residence)
            {
                foreach (Person p2 in residence)
                {
                    if (p1 != p2)
                    {
                        p1.AddConnection(p2, ConnectionType.Close);
                    }
                }
            }
        }

        private static void FriendConnections(Person[] friendGroup)
        {
            foreach (Person p1 in friendGroup)
            {
                foreach (Person p2 in friendGroup)
                {
                    if (p1 != p2)
                    {
                        p1.AddConnection(p2, ConnectionType.Far);
                    }
                }
            }
        }

        public static void DebugPrint(Object message)
        {
            if (Debug)
            {
                Console.WriteLine(message);
            }
        }
    }
}