using System;
using System.Collections;
using CovidSimulator.Simulation;

/*
 * TODO:
 * Test random testing in addition to routine testing
 * Implement pre-symptomatic people
 * Symptoms affect infection rate
 * Random non-compliance to rules (maybe not)
 * Input people in a better way
 * Generate random people arrangements
 * Visualize the data much better, allowing for more data. Do this separately using the exported data.
 * Investigate https://rt.live/ more
 */

namespace CovidSimulator
{
    // TODO: Rewrite all of this. Break it out into more methods. Make it more modular.
    class Program
    {

        public static readonly Random Rand = new Random();

        private const bool Debug = true;

        static void Main(string[] args)
        {
           Simulator simulator = new Simulator();

           simulator.RunSimulations(10000);
           simulator.DisplayResults(0);
           simulator.WriteData(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\data.xml"); // Currently just writing to the desktop because it's easier
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