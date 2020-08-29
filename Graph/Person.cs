using System;

namespace CovidSimulator
{
    /**
     * <summary>
     * A class to encapsulate data related to individual people in the graph
     * </summary>
     */
    public class Person
    {
        private String _name;

        private Symptom _symptom;
        
        private InfectedStatus _status;
        
        private int _daysSinceReceived;

        private int _testDay;

        private bool _willQuarantine = false;

        /**
         * <summary>
         * Initializes a Person object with a specific name and optionally an infection status and length.
         * Sets the test day to a random number between [0, 7)
         * </summary>
         *
         * <param name="name">The name of the person being created</param>
         * <param name="status">The optional infection status of the person</param>
         * <param name="covidLength">The length the person has had covid</param>
         */
        public Person(String name, InfectedStatus status=InfectedStatus.Susceptible, int covidLength=0)
        {
            _name = name;
            _symptom = Symptom.Asymptomatic;
            _status = status;
            _daysSinceReceived = covidLength;
            _testDay = Program.Rand.Next(0, 7);
        }

        /**
         * <summary>
         * Initializes a Person object with a specific name as well as test day and optionally an infection status and length.
         * </summary>
         *
         * <param name="name">The name of the person being created</param>
         * <param name="testDay">The day the person will get tested. Should be in [0, 7)</param>
         * <param name="status">The optional infection status of the person</param>
         * <param name="covidLength">The length the person has had covid</param>
         */
        public Person(String name, int testDay, InfectedStatus status=InfectedStatus.Susceptible, int covidLength=0)
        {
            _name = name;
            _status = status;
            _daysSinceReceived = covidLength;
            _testDay = testDay;
        }
        
        /**
         * <summary>
         * Get the name of this Person object
         * </summary>
         *
         * <returns>The name of this Person object</returns>
         */
        public String GetName()
        {
            return _name;
        }

        /**
         * <summary>Get the symptoms this Person is experiencing</summary>
         *
         * <returns>The symptoms this Person is experiencing</returns>
         */
        public Symptom GetSymptom()
        {
            return _symptom;
        }

        /**
         * <summary>
         * Get the test day of this Person object
         * </summary>
         *
         * <returns>The test day of this Person object</returns>
         */
        public int GetTestDay()
        {
            return _testDay;
        }
        
        /**
         * <summary>
         * Get the number of days since infected for this Person object
         * </summary>
         *
         * <returns>The number of days since infection for this Person object</returns>
         */
        public int GetInfectionDay()
        {
            return _daysSinceReceived;
        }

        /**
         * <summary>
         * Increments the number of days since infection. Called at the end of every day on each infected person.
         * </summary>
         */
        public void UpdateInfectionDay()
        {
            _daysSinceReceived++;
        }

        /**
         * <summary>Returns whether this Person is currently susceptible</summary>
         * <returns>True if this person is currently susceptible, false otherwise.</returns>
         */
        public bool IsSusceptible()
        {
            return _status == InfectedStatus.Susceptible;
        }

        /**
         * <summary>
         * Returns whether this Person is currently infected
         * </summary>
         *
         * <returns>True if this person is currently infected, false otherwise.</returns>
         */
        public bool IsInfected()
        {
            return _status == InfectedStatus.Infected;
        }

        /**
         * <summary>
         * Infects this Person ONLY IF they are currently susceptible
         * </summary>
         */
        public void Infect()
        {
            if (_status == InfectedStatus.Susceptible)
            {
                Program.DebugPrint(_name + " was infected");
                _status = InfectedStatus.Infected;
                _symptom = SymptomRarity.GetSymptom();
                UpdateInfectionDay();
            }
        }

        /**
         * <summary>
         * Set this Person's infection status to recovered.
         * </summary>
         */
        public void Recover()
        {
            Program.DebugPrint(_name + " recovered");
            _status = InfectedStatus.Recovered;
        }

        /**
         * <summary>
         * Mark this Person to be quarantined the next day
         * </summary>
         */
        public void Quarantine()
        {
            _willQuarantine = true;
        }

        /**
         * <summary>
         * Returns whether this person should quarantine.
         * </summary>
         *
         * <returns>True if the person should quarantine.</returns>
         */
        public bool WillQuarantine()
        {
            return _willQuarantine;
        }
    }
}