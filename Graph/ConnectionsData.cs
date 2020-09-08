namespace CovidSimulator
{
    /**
     * <summary>
     * Handles data used to generate the graph
     * </summary>
     */
    public class ConnectionsData
    {
        // Cumulative probabilities
        private double[] _closeProbs;
        private double[] _mediumProbs;
        private double[] _farProbs;
        
        public ConnectionsData()
        {
            // TODO: Read the data from a file, convert it to probabilities
            _closeProbs = new [] {0.1, 0.3, 0.4, 0.7, 0.8, 0.9, 1.0};
            _mediumProbs = new [] {1.0};
            _farProbs = new [] {1.0};
        }

        /**
         * <summary>Gets a random number of connections, based off the data, for a specific connection type</summary>
         * <param name="type">The type of connection to generate the number of connections for.</param>
         * <returns>A random number of connections for this type based off the data</returns>
         */
        public int GetNumConnections(ConnectionType type)
        {
            double num = Program.Rand.NextDouble();

            double[] probs;
            
            switch (type)
            {
                case ConnectionType.Close:
                    probs = _closeProbs;
                    break;
                case ConnectionType.Medium:
                    probs = _mediumProbs;
                    break;
                default:
                    probs = _farProbs;
                    break;
            }

            for (int i = 0; i < probs.Length; i++)
            {
                if (num < probs[i])
                {
                    return i;
                }
            }

            return -1; // This should never happen
        }
    }
}