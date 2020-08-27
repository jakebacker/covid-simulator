namespace CovidSimulator.Simulation
{
    /**
     * <summary>A wrapper to store simulation data, allowing for it to be serialized</summary>
     */
    public class SimulationData
    {
        public int CurrentInfections = 0;
        public int TotalInfections = 0;
        public int MaxInfections = 0;
        public int FurthestPerson = 16;

        public int SimulationDay = 1;
    }
}