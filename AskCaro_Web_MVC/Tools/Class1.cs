﻿using System;
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

    public class Levenshtein
    {
        ///*****************************
        /// Compute Levenshtein distance 
        /// Memory efficient version
        ///*****************************
        public int iLD(String sRow, String sCol)
        {
            int RowLen = sRow.Length;  // length of sRow
            int ColLen = sCol.Length;  // length of sCol
            int RowIdx;                // iterates through sRow
            int ColIdx;                // iterates through sCol
            char Row_i;                // ith character of sRow
            char Col_j;                // jth character of sCol
            int cost;                   // cost

            /// Test string length
            if (Math.Max(sRow.Length, sCol.Length) > Math.Pow(2, 31))
                throw (new Exception("\nMaximum string length in Levenshtein.iLD is " + Math.Pow(2, 31) + ".\nYours is " + Math.Max(sRow.Length, sCol.Length) + "."));

            // Step 1

            if (RowLen == 0)
            {
                return ColLen;
            }

            if (ColLen == 0)
            {
                return RowLen;
            }

            /// Create the two vectors
            int[] v0 = new int[RowLen + 1];
            int[] v1 = new int[RowLen + 1];
            int[] vTmp;



            /// Step 2
            /// Initialize the first vector
            for (RowIdx = 1; RowIdx <= RowLen; RowIdx++)
            {
                v0[RowIdx] = RowIdx;
            }

            // Step 3

            /// Fore each column
            for (ColIdx = 1; ColIdx <= ColLen; ColIdx++)
            {
                /// Set the 0'th element to the column number
                v1[0] = ColIdx;

                Col_j = sCol[ColIdx - 1];


                // Step 4

                /// Fore each row
                for (RowIdx = 1; RowIdx <= RowLen; RowIdx++)
                {
                    Row_i = sRow[RowIdx - 1];


                    // Step 5

                    if (Row_i == Col_j)
                    {
                        cost = 0;
                    }
                    else
                    {
                        cost = 1;
                    }

                    // Step 6

                    /// Find minimum
                    int m_min = v0[RowIdx] + 1;
                    int b = v1[RowIdx - 1] + 1;
                    int c = v0[RowIdx - 1] + cost;

                    if (b < m_min)
                    {
                        m_min = b;
                    }
                    if (c < m_min)
                    {
                        m_min = c;
                    }

                    v1[RowIdx] = m_min;
                }

                /// Swap the vectors
                vTmp = v0;
                v0 = v1;
                v1 = vTmp;

            }

            // Step 7

            /// Value between 0 - 100
            /// 0==perfect match 100==totaly different
            /// 
            /// The vectors where swaped one last time at the end of the last loop,
            /// that is why the result is now in v0 rather than in v1
            //System.Console.WriteLine("iDist=" + v0[RowLen]);
            int max = System.Math.Max(RowLen, ColLen);
            return ((100 * v0[RowLen]) / max);
        }


        ///*****************************
        /// Compute the min
        ///*****************************

        private int Minimum(int a, int b, int c)
        {
            int mi = a;

            if (b < mi)
            {
                mi = b;
            }
            if (c < mi)
            {
                mi = c;
            }

            return mi;
        }

        ///*****************************
        /// Compute Levenshtein distance         
        ///*****************************
        public int LD(String sNew, String sOld)
        {
            int[,] matrix;              // matrix
            int sNewLen = sNew.Length;  // length of sNew
            int sOldLen = sOld.Length;  // length of sOld
            int sNewIdx; // iterates through sNew
            int sOldIdx; // iterates through sOld
            char sNew_i; // ith character of sNew
            char sOld_j; // jth character of sOld
            int cost; // cost

            /// Test string length
            if (Math.Max(sNew.Length, sOld.Length) > Math.Pow(2, 31))
                throw (new Exception("\nMaximum string length in Levenshtein.LD is " + Math.Pow(2, 31) + ".\nYours is " + Math.Max(sNew.Length, sOld.Length) + "."));

            // Step 1

            if (sNewLen == 0)
            {
                return sOldLen;
            }

            if (sOldLen == 0)
            {
                return sNewLen;
            }

            matrix = new int[sNewLen + 1, sOldLen + 1];

            // Step 2

            for (sNewIdx = 0; sNewIdx <= sNewLen; sNewIdx++)
            {
                matrix[sNewIdx, 0] = sNewIdx;
            }

            for (sOldIdx = 0; sOldIdx <= sOldLen; sOldIdx++)
            {
                matrix[0, sOldIdx] = sOldIdx;
            }

            // Step 3

            for (sNewIdx = 1; sNewIdx <= sNewLen; sNewIdx++)
            {
                sNew_i = sNew[sNewIdx - 1];

                // Step 4

                for (sOldIdx = 1; sOldIdx <= sOldLen; sOldIdx++)
                {
                    sOld_j = sOld[sOldIdx - 1];

                    // Step 5

                    if (sNew_i == sOld_j)
                    {
                        cost = 0;
                    }
                    else
                    {
                        cost = 1;
                    }

                    // Step 6

                    matrix[sNewIdx, sOldIdx] = Minimum(matrix[sNewIdx - 1, sOldIdx] + 1, matrix[sNewIdx, sOldIdx - 1] + 1, matrix[sNewIdx - 1, sOldIdx - 1] + cost);

                }
            }

            // Step 7

            /// Value between 0 - 100
            /// 0==perfect match 100==totaly different
            //System.Console.WriteLine("Dist=" + matrix[sNewLen, sOldLen]);
            int max = System.Math.Max(sNewLen, sOldLen);
            return (100 * matrix[sNewLen, sOldLen]) / max;
        }
    }
}