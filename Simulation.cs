using System;
using System.Collections.Generic;

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

        private int _simulationDay = 1;
        
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
                try
                {
                    ProcQuarantine();
                    ProcTest();
                    ProcInfectOthers();
                    ProcUpdateInfection();
                    _simulationDay++;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
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
            for (var p = 0; p < _graph.NumPeople(); p++)
            {
                Person person = _graph.GetPerson(p);

                if (person.WillQuarantine())
                {
                    foreach (ConnEdge connection in _graph.GetConns(p))
                    {
                        _graph.RemoveConnection(connection);
                    }
                }
            }
        }

        /**
         * <summary>
         * Test everyone who is supposed to be tested and set their quarantine flag if they test positive.
         * </summary>
         */
        private void ProcTest()
        {
            for (var p = 0; p < _graph.NumPeople(); p++)
            {
                Person person = _graph.GetPerson(p);

                if ((_simulationDay + person.GetTestDay()) % 7 == 0)
                {
                    if (person.IsInfected())
                    {
                        double num = Program.Rand.NextDouble();

                        if (num <= CovidStatsConfig.FalseNegativeRates[person.GetInfectionDay()])
                        {
                            Program.DebugPrint(person.GetName() + " got a false negative.");
                            continue;
                        }
                        
                        person.Quarantine();
                    }
                }
            }
        }

        /**
         * <summary>
         * For everyone who is infected, try and infect all connections based on weights.
         * </summary>
         */
        private void ProcInfectOthers()
        {
            for (var p = 0; p < _graph.NumPeople(); p++)
            {
                Person person = _graph.GetPerson(p);

                if (person.IsInfected())
                {
                    ConnEdge[] conns = _graph.GetConns(p);

                    foreach (ConnEdge c in conns)
                    {
                        double num = Program.Rand.NextDouble();

                        double infectRate = CovidStatsConfig.InfectionRate * c.Weight;

                        if (num <= infectRate)
                        {
                            Person otherPerson = _graph.GetPerson(c.Other(p));
                            if (otherPerson.IsSusceptible())
                            {
                                otherPerson.Infect();
                                _currentInfections++;
                                _totalInfections++;
                                if (_currentInfections > _maxInfections) _maxInfections = _currentInfections;
                            }
                        }
                    }
                }
            }
        }

        /**
         * <summary>
         * For everyone who is infected, update their infection day count. If it is at the limit, make them recover.
         * </summary>
         */
        private void ProcUpdateInfection()
        {
            for (var p = 0; p < _graph.NumPeople(); p++)
            {
                Person person = _graph.GetPerson(p);
                person.UpdateInfectionDay();
                if (person.GetInfectionDay() >= CovidStatsConfig.AverageLength)
                {
                    person.Recover();
                    _currentInfections--;
                }
            }
        }

        /**
         * <summary>Returns the total number of people infected throughout this simulation</summary>
         * <returns>The total number of people infected throughout the simulation.</returns>
         */
        public int GetTotalInfections()
        {
            return _totalInfections;
        }

        /**
         * <summary>Returns the current day of the simulation</summary>
         * <returns>The current day of the simulation</returns>
         */
        public int GetSimulationDay()
        {
            return _simulationDay;
        }

    }
}