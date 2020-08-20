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
 * Symptom tracking -> Random symptoms that onset at different times with different severities
 */

namespace CovidSimulator
{
    // TODO: Rewrite all of this. Break it out into more methods. Make it more modular.
    class Program
    {

        public static readonly Random Rand = new Random();

        private const bool Debug = false;

        static void Main(string[] args)
        {
           
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