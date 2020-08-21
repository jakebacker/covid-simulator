using System.Collections;
using System.Collections.Generic;

namespace CovidSimulator
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
            return new People(0);
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

                if (!result) return false;
            }
            
            return true;
        }

    }
}