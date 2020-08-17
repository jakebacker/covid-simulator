using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CovidSimulator
{
    /**
     * <summary>
     * An EdgeWeightedGraph
     * Based on implementation from Algorithms, 4th Edition by Robert Sedgewick
     * </summary>
     * */
    public class People
    {
        private readonly int _v;
        private int _e = 0;
        private ConcurrentBag<ConnEdge>[] _adj;
        private Person[] _persons;

        /**
         * <summary>
         * Initializes an empty People object with <paramref name="V"/> people.
         * </summary>
         *
         * <param name="V">The number of people</param>
         * <exception cref="ArgumentException">Thrown when <paramref name="V"/> is less than 0</exception>
         */
        public People(int V)
        {
            if (V < 0) throw new ArgumentException("Number of people must be non-negative");

            _v = V;
            _adj = new ConcurrentBag<ConnEdge>[V];
            _persons = new Person[V];
            for (var v = 0; v < V; v++)
            {
                _adj[v] = new ConcurrentBag<ConnEdge>();
            }
        }

        /**
         * <summary>
         * Returns the number of people in the graph.
         * </summary>
         *
         * <returns>
         * The number of people in the graph.
         * </returns>
         */
        public int NumPeople()
        {
            return _v;
        }

        /**
         * <summary>
         * Add the person <paramref name="p"/> to the list of people at index <paramref name="i"/>
         * </summary>
         *
         * <param name="i">The index to add the person at</param>
         * <param name="p">The person to add</param>
         */
        public void AddPerson(int i, Person p)
        {
            _persons[i] = p;
        }

        /**
         * <summary>
         * Get the person at the index <paramref name="i"/>
         * </summary>
         *
         * <param name="i">The index of the person</param>
         * <returns>The Person requested</returns>
         */
        public Person GetPerson(int i)
        {
            return _persons[i];
        }

        /**
         * <summary>
         * Confirms that the person <paramref name="v"/> exists in the graph.
         * </summary>
         *
         * <param name="v">The person to check</param>
         * <exception cref="ArgumentException">Thrown when the person does not exist in the graph</exception>
         */
        private void ValidatePerson(int v)
        {
            if (v < 0 || v >= _v)
            {
                throw new ArgumentException("Person " + v + " is not between 0 and " + (_v-1));
            }
        }

        /**
         * <summary>
         * Adds the edge <paramref name="e"/> to the graph.
         * </summary>
         *
         * <param name="e">The edge to add</param>
         * <exception cref="ArgumentException">Thrown unless both endpoints are between <code>0</code> and <code>V-1</code></exception>
         */
        public void AddConnection(ConnEdge e)
        {
            int v = e.Either();
            int w = e.Other(v);
            ValidatePerson(v);
            ValidatePerson(w);
            
            _adj[v].Add(e);
            _adj[w].Add(e);
            _e++;
        }

        /**
         * <summary>
         * Returns the connections from the person <paramref name="v"/>
         * </summary>
         *
         * <param name="v">The person</param>
         * <returns>The connections this person has as an IEnumerator</returns>
         * <exception cref="ArgumentException">Thrown unless <code>0 &lt;= v &lt; V</code></exception>
         */
        public IEnumerator<ConnEdge> GetConns(int v)
        {
            ValidatePerson(v);
            return _adj[v].GetEnumerator();
        }
    }
}