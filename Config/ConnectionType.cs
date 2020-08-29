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
    
    public class ConnectionValues
    {
        public static readonly double[] InfectionRates = {1, 0.31, 0.01};
        
        public static readonly double Close = InfectionRates[0]; // No restriction
        public static readonly double Medium = InfectionRates[1];  // https://www.cbs58.com/news/doctors-on-the-mask-debate-recent-studies-prove-effectiveness-of-masks-for-covid-19
        public static readonly double Far = InfectionRates[2]; // Can't find a source, but it's low
    }
}