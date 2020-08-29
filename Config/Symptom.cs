using System;

namespace CovidSimulator
{
    /**
     * <summary>This enum marks the possible symptom categories a person can experience</summary>
     */
    public enum Symptom
    {
        Asymptomatic,
        Mild,
        Moderate,
        Severe
    }

    /**
     * This class is used to store the rarities of the symptoms
     */
    public class SymptomRarity
    {
        // These should be formatted as cumulative probabilities. Ex: [0.4, 0.7, 0.9, 1.0]
        // Geared towards college students
        // https://www.nature.com/articles/s41591-020-0962-9    
        // TODO: Make these numbers real. I CANNOT find accurate numbers
        public static readonly double[] SymptomRarities = {0.69, 0.90, 0.97, 1.0};

        public static readonly double Asymptomatic = SymptomRarities[0];
        public static readonly double Mild = SymptomRarities[1];
        public static readonly double Moderate = SymptomRarities[2];
        public static readonly double Severe = SymptomRarities[3];

        /**
         * <summary>Get a random symptom based off the rarities</summary>
         *
         * <returns>A random symptom</returns>
         */
        public static Symptom GetSymptom()
        {
            double num = Program.Rand.NextDouble();

            for (int i = 0; i < SymptomRarities.Length; i++)
            {
                if (num < SymptomRarities[i])
                {
                    return (Symptom)i;
                }
            }
            
            throw new Exception("Invalid SymptomRarities! SymptomRarities do not add up to 1.0");
        }
    }

    /**
     * This class is used to store how prominent the symptoms are for symptom tracking
     */
    public class SymptomProminence
    {
        // TODO: Get a basis for these numbers. Gonna be hard until I get real data.
        public static readonly double[] SymptomProminences = {0, 0.5, 1, 1};

        public static readonly double Asymptomatic = SymptomProminences[0];
        public static readonly double Mild = SymptomProminences[1];
        public static readonly double Moderate = SymptomProminences[2];
        public static readonly double Severe = SymptomProminences[3];
    }
}