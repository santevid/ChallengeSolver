using System;
using System.Linq;
using System.Collections.Generic;
// you can also use other imports, for example:
// using System.Collections.Generic;
class Solution
{
    private int[] A;
    private int[] B;
    private int[] C;
    private List<int>[] listve;

    private int[] countAB;
    private int[] countBA;

    private int traverse(int vertex, int previousEdgeIndex)
    {
        int maxLength = 0;

        int k = 0;
        foreach(var nextEdgeIndex in listve[vertex])
        {
            int pathLength = 0;

            if (previousEdgeIndex < 0 || (C[previousEdgeIndex] < C[nextEdgeIndex]))
            {
                int nextVertex = vertex;
                int path = 0;

                if (A[nextEdgeIndex] == vertex)
                {
                    nextVertex = B[nextEdgeIndex];
                    path = countAB[nextEdgeIndex];
                }
                else
                {
                    nextVertex = A[nextEdgeIndex];
                    path = countBA[nextEdgeIndex];
                }

                pathLength = (path > 0) ? path : 1 + traverse(nextVertex, nextEdgeIndex);

                if (A[nextEdgeIndex] == vertex) countAB[nextEdgeIndex] = pathLength;
                else countBA[nextEdgeIndex] = pathLength;

                if (maxLength < pathLength) maxLength = pathLength;
                if (maxLength == A.Length) return maxLength;
            }
            else pathLength = 0;
        }

        return maxLength;
    }

    public int solution(int N, int[] A, int[] B, int[] C)
    {
        int M = A.Length;
        this.A = A;
        this.B = B;
        this.C = C;

        listve = new List<int>[N];
        countAB = new int[M];
        countBA = new int[M];

        for (int i = 0; i < N; i++) listve[i] = new List<int>();

        for (int i = 0; i < M; i++)
        {
            listve[A[i]].Add(i);
            listve[B[i]].Add(i);
        }


        int max = 0;
        for (int i = 0; i < N; i++)
        {
            int pathL = traverse(i, -1);
            if (pathL > max) max = pathL;
            if (max == M) return max;
        }

        return max;
    }
}