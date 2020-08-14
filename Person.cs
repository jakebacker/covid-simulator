using System;
using System.Collections;

namespace CovidSimulator
{
    public class Person
    {
        private String _name;
        
        private InfectedStatus _status;

        private ArrayList _connections = new ArrayList();
        
        private ArrayList _connectionPeople = new ArrayList();

        private int _daysSinceReceived;

        private int _testDay;

        private bool _willQuarantine = false;

        public Person(String name, InfectedStatus status=InfectedStatus.Susceptible, int covidLength=-1)
        {
            _name = name;
            _status = status;
            _daysSinceReceived = covidLength;
            _testDay = Program.Rand.Next(0, 7);
        }

        public String GetName()
        {
            return _name;
        }

        public int GetTestDay()
        {
            return _testDay;
        }

        public void AddConnection(Person person2, ConnectionType type)
        {
            if (!_connectionPeople.Contains(person2)) // Only add the connection if one doesn't already exist
            {
                Connection conn = new Connection(this, person2, type);
                _connectionPeople.Add(person2);

                _connections.Add(conn);
            }
        }

        public ArrayList GetConnections()
        {
            return _connections;
        }

        public void RemoveConnections()
        {
            foreach (Connection conn in _connections) {
                conn.Delete();
            }

            _connections = new ArrayList();
        }

        public void RemoveConnection(Connection conn)
        {
            _connections.Remove(conn);
        }

        public int GetLastUpdate()
        {
            return _daysSinceReceived;
        }

        public void UpdateDay()
        {
            _daysSinceReceived++;
        }

        public bool IsInfected()
        {
            return _status == InfectedStatus.Infected;
        }

        public void Infect()
        {
            if (_status == InfectedStatus.Susceptible)
            {
                Program.DebugPrint(_name + " was infected");
                Program.Infected++;
                Program.TotalInfected++;
                _status = InfectedStatus.Infected;
                UpdateDay();
            }
        }

        public void Recover()
        {
            Program.Infected--;
            Program.DebugPrint(_name + " recovered");
            _status = InfectedStatus.Recovered;
        }

        public void Quarantine()
        {
            _willQuarantine = true;
        }

        public bool WillQuarantine()
        {
            return _willQuarantine;
        }

        public void UpdateConnections()
        {
            foreach (Connection conn in _connections)
            {
                conn.UpdatePeople();
            }
        }
    }
}