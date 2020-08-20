using System;

namespace CovidSimulator
{
    [Obsolete("Connection is deprecated. No longer needed with new People class.")]
    public class Connection
    {

        private PersonOld personOne;
        private PersonOld personTwo;
        private ConnectionType type;

        public Connection(PersonOld personOldOne, PersonOld personTwo, ConnectionType type)
        {
            this.personOne = personOldOne;
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

        public PersonOld GetOtherPerson()
        {
            return personTwo;
        }

    }
}