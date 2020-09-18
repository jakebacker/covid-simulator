namespace CovidSimulator.Utility
{
    public class ArrayHelper
    {
        /**
         * <summary>
         * Find the max value in each column in a non-jagged 2D array.
         * </summary>
         * <param name="arr">The array to find the max values of</param>
         * <returns>An array containing the max values in each column</returns>
         */
        public static int[] FindMaxColumns(int[][] arr)
        {
            int len = arr[0].Length;
            
            int[] maxes = new int[len];
            
            foreach (int[] a in arr) {
                for (int i = 0; i < len; i++)
                {
                    if (a[i] > maxes[i])
                    {
                        maxes[i] = a[i];
                    }
                }
            }

            return maxes;
        }
    }
}