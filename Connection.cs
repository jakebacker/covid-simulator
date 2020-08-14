using System;

namespace CovidSimulator
{
    public class Connection
    {

        private Person personOne;
        private Person personTwo;
        private ConnectionType type;

        public Connection(Person personOne, Person personTwo, ConnectionType type)
        {
            this.personOne = personOne;
            this.personTwo = personTwo;
            this.type = type;
        }

        public void UpdatePeople()
        {
            double num = Program.Rand.NextDouble();

            double infectRate = CovidStatsConfig.InfectionRate * ConnectionValues.InfectionRates[(int) type];

            if (num <= infectRate)
            {
                personTwo.Infect();
            }
        }

        public void Delete()
        {
            personTwo.RemoveConnection(this);
        }

        public Person GetOtherPerson()
        {
            return personTwo;
        }

    }
}