using System;

namespace CovidSimulator
{
    public class ConnEdge
    {

        private readonly int _v;
        private readonly int _w;
        public readonly double Weight;

        /**
         * <summary>
         * Initializes a connection between People <paramref name="v"/> and <paramref name="w"/> with the infection modifier
         * <paramref name="weight"/>
         * </summary>
         *
         * <param name="v">One person</param>
         * <param name="w">The other person</param>
         * <param name="weight">The infection modifier of the connection</param>
         * <exception cref="ArgumentException">Thrown if either <paramref name="v"/> or <paramref name="w"/> is a negative integer</exception>
         * <exception cref="ArgumentException">Thrown if <paramref name="weight"/> is NaN or less than 0</exception>
         */
        public ConnEdge(int v, int w, double weight)
        {
            if (v < 0) throw new ArgumentException("v must be a non-negative integer");
            if (w < 0) throw new ArgumentException("w must be a non-negative integer");
            if (Double.IsNaN(weight)) throw new ArgumentException("weight is NaN");
            if (weight < 0) throw new ArgumentException("weight must be non-negative");
            _v = v;
            _w = w;
            Weight = weight;
        }
        
        /**
         * <summary>
         * Returns either endpoint of the connection. Technically arbitrary.
         * </summary>
         *
         * <returns>Either endpoint of the connection</returns>
         */
        public int Either()
        {
            return _v;
        }
        
        /**
         * <summary>
         * Returns the endpoint that is opposite of <paramref name="person"/>
         * </summary>
         *
         * <param name="person">The opposite endpoint to the one you are getting</param>
         * <returns>The opposite endpoint to the input</returns>
         */
        public int Other(int person)
        {
            if (person == _v) return _w;
            if (person == _w) return _v;
            throw new ArgumentException("Illegal endpoint");
        }
    }
}