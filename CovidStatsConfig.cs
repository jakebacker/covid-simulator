namespace CovidSimulator
{
    public class CovidStatsConfig
    {
        // This is likely a very large overestimate
        // https://covidactnow.org/us/ma?s=863169
        public static readonly double R0 = 2.35;
        public static readonly double AverageLength = 8;
        public static readonly double InfectionRate = R0 / AverageLength; // Infections per day
        
        // https://www.healio.com/news/primary-care/20200609/rtpcr-yields-high-falsenegative-rates-in-early-sarscov2-infection
        // This may be wildly inaccurate. Can't really tell though
        public static readonly double[] FalseNegativeRates = {1, .89, .78, .67, .38, .32, .26, .2, 0.21, 0.66};
    }
}