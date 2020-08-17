using System;

namespace CovidSimulator
{
    [Obsolete("ConnectionType is deprecated. No longer needed with new People class.")]
    public enum ConnectionType
    {
        Close, // No restriction 
        Medium,
        Far
    }

    [Obsolete("ConnectionValues is deprecated. No longer needed with new People class.")]
    public class ConnectionValues
    {
        public static readonly double[] InfectionRates = {1, 0.31, 0.01};
        
        public static readonly double Close = InfectionRates[(int)ConnectionType.Close]; // No restriction
        public static readonly double Medium = InfectionRates[(int)ConnectionType.Medium];  // https://www.cbs58.com/news/doctors-on-the-mask-debate-recent-studies-prove-effectiveness-of-masks-for-covid-19
        public static readonly double Far = InfectionRates[(int)ConnectionType.Far]; // Can't find a source, but it's low
    }
}