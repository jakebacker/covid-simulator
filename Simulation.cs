namespace CovidSimulator
{
    /**
     * <summary>
     * This class is responsible for handling a single simulation.
     * It will run the simulation as well as collect data for the particular sim that can later be used for data analysis.
     * </summary>
     */
    public class Simulation
    {
        private People _graph;
        
        public Simulation(People graph)
        {
            _graph = graph;
        }

        /**
         * <summary>
         * Run a simulation based on this object.
         * </summary>
         *
         * <returns>True if the simulation was successful</returns>
         */
        public bool RunSimulation()
        {

            return true;
        }
    }
}