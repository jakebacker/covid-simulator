using System;
using System.Linq;

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
        
        /**
         * <summary>
         * Creates a new ConnectionsData object based on the sample data <paramref name="connData"/>
         * </summary>
         * <param name="connData">The data to create the model off of</param>
         */
        public ConnectionsData(int[][] connData)
        {
            _closeProbs = new double[connData.Length]; // The max length will be connData.Length
            _mediumProbs = new double[connData.Length];
            _farProbs = new double[connData.Length];
            
            // Count the number of each number of connections of each type
            foreach (int[] c in connData)
            {
                _closeProbs[c[0]] += 1;
                _mediumProbs[c[1]] += 1;
                _farProbs[c[2]] += 1;
            }
            
            // Convert the numbers to cumulative probabilities
            double previousClose = 0;
            double previousMedium = 0;
            double previousFar = 0;
            for (int i = 0; i < connData.Length; i++)
            {
                _closeProbs[i] = previousClose + (_closeProbs[i] / connData.Length);
                _mediumProbs[i] = previousMedium + (_mediumProbs[i] / connData.Length);
                _farProbs[i] = previousFar + (_farProbs[i] / connData.Length);
                
                previousClose = _closeProbs[i];
                previousMedium = _mediumProbs[i];
                previousFar = _farProbs[i];
            }
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

        /**
         * <summary>
         * Generate a 2D array of the connection data based off the file <paramref name="file"/>
         * </summary>
         *
         * <param name="file">The file to input</param>
         * <returns>The parsed data as a 2D integer array</returns>
         */
        public static int[][] GenerateConnData(String file)
        {
            string[] lines = System.IO.File.ReadAllLines(file);

            int[][] connData = new int[][lines.Length-1];

            int index = 0;
            
            foreach (String line in lines.Skip(1))
            {
                string[] split = line.Split(",");

                int[] lineData = new[] {Int32.Parse(split[2]), Int32.Parse(split[3]), Int32.Parse(split[4])};

                connData[index++] = lineData;
            }

            return connData;
        }
    }
}