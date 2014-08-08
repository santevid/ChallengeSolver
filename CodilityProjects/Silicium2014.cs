using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityProjects
{
    /// <summary>
    /// Solution for Codility Challenge Silicium 2014
    /// Find K-th largest element in sA*(sB^T) matrix, where sA and sB are vectors of length n.
    /// Complexity of solution O(n*log(X*Y) == O(n*(log(X)+log(y))) == O(n*(log(X + Y)))
    /// https://codility.com/programmers/challenges/silicium2014
    /// </summary>
    public class Silicium2014
    {
        private class ReverseComparer:IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return y-x;
            }
        }

        private ReverseComparer comparer = new ReverseComparer();

        //new Silicium2014().Solution(6, 7, 3, new int[] { 1, 3 }, new int[] { 1, 5 })
        public int Solution(int X, int Y, int K, int[] A, int[] B)
        {
            //transform input in more approprite problem
            //create cordinate pieces
            int[] sA = GetSizes(A, X);
            int[] sB = GetSizes(B, Y);

            int N = sA.Length;

            //sort pices in descending order
            Array.Sort(sA, comparer);
            Array.Sort(sB, comparer);

            int max = (sA[0] * sB[0]);
            int min = (sA[N-1] * sB[N-1]);

            int half = (max + min)/2;

            while(true)
            {
                int num = RankPositive(sA, sB, half);

                int halfLast = half;
                if (num > K)
                {
                    min = half;
                    half = (max + half) / 2;
                }
                else if (num < K)
                {
                    max = half;
                    half = (min + half) / 2;
                }
                else
                {
                    //find first number larger than half
                    return FirstElementAfter(sA, sB, half);
                }

                if (halfLast == half) return Math.Max(max, half);

            }
        }

        /// <summary>
        /// Find first element in sA*(sB^T) matrix, larger than <paramref name="num"/>
        /// </summary>
        /// <param name="sA">First vector</param>
        /// <param name="sB">Second vector</param>
        /// <param name="num"></param>
        /// <returns>First larger element</returns>
        private int FirstElementAfter(int[] sA, int[] sB, int num)
        {
            int n = sA.Length;
            int r = 0;
            int j = n - 1;

            int minHigher = sA[0] * sB[0];
            for (int i = 0; i < n; i++)
            {
                while (j >= 0 && sA[i] * sB[j] <= num) j = j - 1;
                if (j >= 0) minHigher = Math.Min(minHigher, sA[i] * sB[j]);
            }

            return minHigher;
        }

        /// <summary>
        /// Calculates number of elements that are larger than p.
        /// Complexity: O(n)
        /// </summary>
        /// <param name="sA">First vector</param>
        /// <param name="sB">Second vector</param>
        /// <param name="p">Number of elements larger than this number</param>
        /// <returns>Number of elements larger than <paramref name="p"/></returns>
        private int RankPositive(int[] sA, int[] sB, int p)
        {

            int n = sA.Length;
            int r = 0;
            int j = n - 1;

            for (int i = 0; i < n; i++)
            {
                while (j >= 0 && sA[i] * sB[j] <= p) j = j - 1;
                r = r + j + 1;
            }

            return r;
        }

        /// <summary>
        /// Calculates distances between adjenct elements in array.
        /// </summary>
        /// <param name="arr">Array with coordinates</param>
        /// <param name="border">Upper border, lower border is fixed to zero.</param>
        /// <returns>Array with distances between elements in <param name="arr"</returns>
        private int[] GetSizes(int[] arr, int upperBorder)
        {
            int[] sizes = new int[arr.Length + 1];

            int last = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sizes[i] = arr[i] - last;
                last = arr[i];
            }

            sizes[arr.Length] = upperBorder - last;

            return sizes;
        }
    }
}
