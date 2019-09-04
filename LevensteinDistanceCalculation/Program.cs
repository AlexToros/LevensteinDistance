using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevensteinDistanceCalculation
{
    public class Program
    {
        static void Main(string[] args)
        {
        }

        public static int GetLevensteinDistance(string first, string second)
        {
            first = first.ToLower();
            second = second.ToLower();
            if (first.Equals(second) || string.IsNullOrEmpty(first) && string.IsNullOrEmpty(second)) return 0;
            if (string.IsNullOrEmpty(first)) return second.Length;
            if (string.IsNullOrEmpty(second)) return first.Length;

            int cost;
            int[,] matrix = new int[first.Length + 1, second.Length + 1];
            for (int i = 0; i < matrix.GetLength(0); i++)
                matrix[i, 0] = i;
            for (int i = 0; i < matrix.GetLength(1); i++)
                matrix[0, i] = i;

            int min1, min2, min3;
            for (int i = 1; i < matrix.GetLength(0); i++)
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    cost = !(first[i - 1] == second[j - 1]) ? 1 : 0;

                    min1 = matrix[i - 1, j] + 1;
                    min2 = matrix[i, j - 1] + 1;
                    min3 = matrix[i - 1, j - 1] + cost;
                    matrix[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            return matrix[matrix.GetUpperBound(0), matrix.GetUpperBound(1)];
        }
    }
}
