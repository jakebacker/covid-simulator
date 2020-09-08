using System;
using System.Collections;
using CovidSimulator.Simulation;

/*
 * TODO:
 * Improve random graphs by making them more representative
 * Test random testing in addition to routine testing. Note: This doesn't actually matter stats wise. The data would not be significant.
 * Implement pre-symptomatic people
 * Symptoms affect infection rate
 * Random non-compliance to rules (maybe not)
 * Input people in a better way
 * Visualize the data much better, allowing for more data. Do this separately using the exported data.
 * Investigate https://rt.live/ more
 */

namespace CovidSimulator
{
    class Program
    {
        public static readonly Random Rand = new Random();

        public static ConnectionsData data;

        private const bool Debug = true;

        static void Main(string[] args)
        {
            data = new ConnectionsData();

            Simulator simulator = new Simulator();

            simulator.RunSimulations(10000);
            simulator.DisplayResults(0);
            simulator.WriteData(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +
                                "\\data.xml"); // Currently just writing to the desktop because it's easier
        }

        public static void DebugPrint(Object message)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (Debug)
            {
                Console.WriteLine(message);
            }
        }
    }
}