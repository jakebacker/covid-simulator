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
        private People GenerateGraph()
        {
            // Currently very gross. Will be slightly less gross when converted to random generation
            
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
            int [] residence2 = {5, 6, 7, 8, 9};

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
                person0, person1, person2, person3, person4, person5, person6, person7, person8, person9, person10, person11, person12, person13, person14, person15, person16,
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
                People genGraph = GenerateGraph();
                Simulation sim = new Simulation(genGraph);
                
                simulations.Add(sim);

                bool result = sim.RunSimulation();

                if (!result)
                {
                    Console.WriteLine("Simulation Failed! Returning...");
                    return false;
                }
            }
            
            Console.WriteLine("Simulations Complete.");
            
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