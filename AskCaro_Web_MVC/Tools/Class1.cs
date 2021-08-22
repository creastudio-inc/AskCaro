using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskCaro_Web_MVC.Tools
{
    public class Class1
    {
        public static int LevenshteinDistance(string src, string dest)
        {
            int[,] d = new int[src.Length + 1, dest.Length + 1];
            int i, j, cost;
            char[] str1 = src.ToCharArray();
            char[] str2 = dest.ToCharArray();

            for (i = 0; i <= str1.Length; i++)
            {
                d[i, 0] = i;
            }
            for (j = 0; j <= str2.Length; j++)
            {
                d[0, j] = j;
            }
            for (i = 1; i <= str1.Length; i++)
            {
                for (j = 1; j <= str2.Length; j++)
                {

                    if (str1[i - 1] == str2[j - 1])
                        cost = 0;
                    else
                        cost = 1;

                    d[i, j] =
                        Math.Min(
                            d[i - 1, j] + 1,              // Deletion
                            Math.Min(
                                d[i, j - 1] + 1,          // Insertion
                                d[i - 1, j - 1] + cost)); // Substitution

                    if ((i > 1) && (j > 1) && (str1[i - 1] ==
                        str2[j - 2]) && (str1[i - 2] == str2[j - 1]))
                    {
                        d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
                    }
                }
            }

            return d[str1.Length, str2.Length];
        }

       
    }
    public static class LevenshteinDistance
    {
        /// <summary>
        ///     Calculate the difference between 2 strings using the Levenshtein distance algorithm
        /// </summary>
        /// <param name="source1">First string</param>
        /// <param name="source2">Second string</param>
        /// <returns></returns>
        public static int Calculate(string source1, string source2) //O(n*m)
        {
            var source1Length = source1.Length;
            var source2Length = source2.Length;

            var matrix = new int[source1Length + 1, source2Length + 1];

            // First calculation, if one entry is empty return full length
            if (source1Length == 0)
                return source2Length;

            if (source2Length == 0)
                return source1Length;

            // Initialization of matrix with row size source1Length and columns size source2Length
            for (var i = 0; i <= source1Length; matrix[i, 0] = i++) { }
            for (var j = 0; j <= source2Length; matrix[0, j] = j++) { }

            // Calculate rows and collumns distances
            for (var i = 1; i <= source1Length; i++)
            {
                for (var j = 1; j <= source2Length; j++)
                {
                    var cost = (source2[j - 1] == source1[i - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }
            // return result
            return matrix[source1Length, source2Length];
        }
    }
}