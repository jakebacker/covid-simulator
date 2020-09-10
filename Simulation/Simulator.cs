using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CovidSimulator.Simulation
{
    /**
     * <summary>
     * This class is responsible for managing all of the Simulations run.
     * This will generate the graphs to be used for each simulation and run the requested number as well as compile the results together.
     * </summary>
     */
    public class Simulator
    {

        private List<Simulation> simulations = new List<Simulation>();

        // Use this to initialize any sort of settings
        public Simulator()
        {
            
        }
        
        /**
         * <summary>
         * Generate a graph of people
         * </summary>
         *
         * <returns>The generated graph</returns>
         */
        private People GenerateGraph(bool genStatic)
        {

            if (genStatic)
            {
                Person person0 = new Person("Person0");
                Person person1 = new Person("Person1");
                Person person2 = new Person("Person2");
                Person person3 = new Person("Person3");
                Person person4 = new Person("Person4");
                int[] residence1 = {0, 1, 2, 3, 4};

                Person person5 = new Person("Person5");
                Person person6 = new Person("Person6");
                Person person7 = new Person("Person7");
                Person person8 = new Person("Person8");
                Person person9 = new Person("Person9");
                int[] residence2 = {5, 6, 7, 8, 9};

                Person person10 = new Person("Person10");
                Person person11 = new Person("Person11");
                Person person12 = new Person("Person12");
                Person person13 = new Person("Person13");
                Person person14 = new Person("Person14");
                int[] residence3 = {10, 11, 12, 13, 14};

                Person person15 = new Person("Person15");
                Person person16 = new Person("Person16");
                Person person17 = new Person("Person17");
                int[] residence4 = {15, 16, 17};

                Person person18 = new Person("Person18");

                Person person19 = new Person("Person19");

                Person[] peopleArr =
                {
                    person0, person1, person2, person3, person4, person5, person6, person7, person8, person9, person10,
                    person11, person12, person13, person14, person15, person16,
                    person17, person18, person19
                };

                People people = new People(peopleArr);

                ConnEdge[] r1Conns = People.ResidenceConnections(residence1);
                ConnEdge[] r2Conns = People.ResidenceConnections(residence2);
                ConnEdge[] r3Conns = People.ResidenceConnections(residence3);
                ConnEdge[] r4Conns = People.ResidenceConnections(residence4);

                foreach (ConnEdge e in r1Conns)
                {
                    people.AddConnection(e);
                }

                foreach (ConnEdge e in r2Conns)
                {
                    people.AddConnection(e);
                }

                foreach (ConnEdge e in r3Conns)
                {
                    people.AddConnection(e);
                }

                foreach (ConnEdge e in r4Conns)
                {
                    people.AddConnection(e);
                }

                people.AddConnection(new ConnEdge(0, 5, ConnectionValues.Medium));
                people.AddConnection(new ConnEdge(6, 10, ConnectionValues.Medium));
                people.AddConnection(new ConnEdge(14, 15, ConnectionValues.Medium));
                people.AddConnection(new ConnEdge(16, 18, ConnectionValues.Medium));

                people.AddConnection(new ConnEdge(7, 19, ConnectionValues.Medium));

                person18.Infect();

                return people;
            }

            int numPeople = 10; // Small for now so that I can figure out if this works.

            Person[] randPeopleArr = new Person[numPeople];

            int[][] closeAdj = new int[numPeople][];
            int[][] mediumAdj = new int[numPeople][];
            int[][] farAdj = new int[numPeople][];
            
            int[] closeSeq = new int[numPeople];
            int[] mediumSeq = new int[numPeople];
            int[] farSeq = new int[numPeople];
            
            for (int i = 0; i < numPeople; i++)
            {
                randPeopleArr[i] = new Person("Person" + i);
                // Infect people here at some point
                closeAdj[i] = new int[numPeople];
                mediumAdj[i] = new int[numPeople];
                farAdj[i] = new int[numPeople];
                
                for (int j = 0; j < numPeople; j++)
                {
                    closeAdj[i][j] = 0;
                    mediumAdj[i][j] = 0;
                    farAdj[i][j] = 0;
                }

                closeSeq[i] = Program.data.GetNumConnections(ConnectionType.Close);
                mediumSeq[i] = Program.data.GetNumConnections(ConnectionType.Medium);
                farSeq[i] = Program.data.GetNumConnections(ConnectionType.Far);
            }

            // This may not be entirely accurate. I might need to make this so it generates the close, medium, and far numbers together (something something dependence)
            // After generating a ton of random graphs (see below), select the one that has a density that's closest to the data
            for (int i = 0; i < numPeople; i++)
            {
                for (int j = i + 1; j < numPeople; j++) // Make this generate random graphs by randomizing the j order. Don't just go sequentially. 
                {
                    if (closeSeq[i] <= 0)
                    {
                        break;
                    }

                    if (closeSeq[j] > 0 && closeAdj[i][j] != 1)
                    {
                        // Connect them!
                        closeSeq[i]--;
                        closeSeq[j]--;
                        closeAdj[i][j] = 1;
                        closeAdj[j][i] = 1;
                    }
                }
            }

            People randPeople = new People(randPeopleArr);

            return randPeople;
        }

        
        // This is like, really, really bad right now. Try to figure out something else
        /**
         * <summary>Get the global clustering coefficient for the graph supplied. This is used to determine
         * the "density" of a graph and then if it is close enough to the data.</summary>
         * <param name="adj">The adjacency matrix of the graph</param>
         * <returns>The clustering coefficient of the graph supplied</returns>
         */
        public static double GetClustering(int[][] adj)
        {
            // Denominator first, because of special condition
            double denominator = 0;

            for (int i = 0; i < adj.Length; i++)
            {
                int k = 0;
                for (int j = 0; j < adj.Length; j++)
                {
                    k += adj[i][j];
                }

                denominator += k * (k - 1);
            }

            if (denominator < 1) return 0; // Special case

            double numerator = 0;
            
            for (int i = 0; i < adj.Length; i++)
            {
                for (int j = 0; j < adj.Length; j++)
                {
                    for (int k = 0; k < adj.Length; k++)
                    {
                        numerator += adj[i][j] * adj[j][k] * adj[k][i];
                    }
                }
            }

            return numerator / denominator;
        }


        /**
         * <summary>
         * Run <paramref name="numSims"/> simulations and return if they succeeded.
         * Data from the simulations will be stored in and accessed using this object.
         * </summary>
         *
         * <param name="numSims">The number of simulations to run</param>
         * <returns>True when the simulations succeeded</returns>
         */
        public bool RunSimulations(int numSims=1)
        {
            for (var i = 0; i < numSims; i++)
            {
                Program.DebugPrint("Simulation " + i);
                People genGraph = GenerateGraph(false);
                Simulation sim = new Simulation(genGraph);
                
                simulations.Add(sim);

                bool result = sim.RunSimulation();

                if (!result)
                {
                    Console.WriteLine("Simulation Failed! Returning...");
                    return false;
                }
            }
            
            Console.WriteLine("Simulations Complete.\n");
            
            return true;
        }

        /**
         * <summary>Prints the results of the simulations to the console</summary>
         */
        public void DisplayResults()
        {
            double averageTotalInfections = 0;
            double averageSimLength = 0;
            
            foreach (Simulation s in simulations)
            {
                averageTotalInfections += s.GetTotalInfections();
                averageSimLength += s.GetSimulationDay();
            }

            averageTotalInfections /= simulations.Count;
            averageSimLength /= simulations.Count;

            Console.WriteLine("Average Total Infections: " + averageTotalInfections);
            Console.WriteLine("Average Simulation Length: " + averageSimLength);
            Console.WriteLine();
        }

        public void DisplayResults(int farPerson)
        {
            DisplayResults();

            int num = 0;

            foreach (Simulation s in simulations)
            {
                if (s.GetSimulationData().FurthestPerson <= farPerson)
                {
                    num++;
                }
            }

            Console.WriteLine(num + " simulations reached person " + farPerson);
        }

        /**
         * <summary>Write the data from the simulations to a file</summary>
         * <param name="output">The output file location</param>
         */
        public void WriteData(String output)
        {
            Console.WriteLine("Writing all simulation data to file at " + output);
            List<SimulationData> dataList = new List<SimulationData>();

            foreach (Simulation s in simulations)
            {
                dataList.Add(s.GetSimulationData());
            }

            SimulationData[] dataArr = dataList.ToArray();

            var xmls = new XmlSerializer(typeof(SimulationData[]));
            
            xmls.Serialize(new XmlTextWriter(output, Encoding.UTF8), dataArr);

            Console.WriteLine("Data exported");
        }

    }
}