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

        private int _currentInfections = 0;
        private int _totalInfections = 0;
        private int _maxInfections = 0;
        
        public Simulation(People graph)
        {
            _graph = graph;

            _currentInfections = FindInfections();
            _totalInfections = _currentInfections;
            _maxInfections = _currentInfections;
        }

        /**
         * <summary>
         * Search through the entire graph to find all infected people
         * </summary>
         *
         * <returns>The number of people currently infected</returns>
         */
        private int FindInfections()
        {
            int infections = 0;
            for (var p = 0; p < _graph.NumPeople(); p++)
            {
                if (_graph.GetPerson(p).IsInfected())
                {
                    infections++;
                }
            }

            return infections;
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
            while (_currentInfections > 0)
            {
                // This is definitely not the fastest way, but it makes it more modular and such.
                ProcQuarantine();
                ProcTest();
                ProcInfectedTasks();
            }

            return true;
        }

        /**
         * <summary>
         * Check everyone to see if they need to quarantine. If they do, remove all connections.
         * </summary>
         */
        private void ProcQuarantine()
        {
            
        }

        /**
         * <summary>
         * Test everyone who is supposed to be tested and set their quarantine flag if they test positive.
         * </summary>
         */
        private void ProcTest()
        {
            
        }

        /**
         * <summary>
         * Wrapper for <see cref="ProcInfectOthers"/> and <see cref="ProcUpdateInfection"/>
         * </summary>
         */
        private void ProcInfectedTasks()
        {
            ProcInfectOthers();
            ProcUpdateInfection();
        }

        /**
         * <summary>
         * For everyone who is infected, try and infect all connections based on weights.
         * </summary>
         */
        private void ProcInfectOthers()
        {
            
        }

        /**
         * <summary>
         * For everyone who is infected, update their infection day count. If it is at the limit, make them recover.
         * </summary>
         */
        private void ProcUpdateInfection()
        {
            
        }

    }
}