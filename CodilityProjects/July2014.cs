using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityProjects
{

    class July2014 {
        public int solution(int[] A) {
            int N = A.Length;
            if (N == 1) return A[0];

            int[] maxForward = new int[N+2];

            int maxSum = -10001;
            int maxInd = 0;
            for(int i = 1; i <= N; i++)
            {
                maxForward[i] = Math.Max(maxForward[i-1] + A[i-1], A[i-1]);
                if(maxForward[i] > maxSum)
                {
                    maxSum = maxForward[i];
                    maxInd = i - 1;
                }
            }
        
            //find borders
            int left = maxInd;
            int right = maxInd;
        
            int sumLeft = 0;
            for(int i=maxInd; i >=0; i--)
            {
                sumLeft += A[i];
                if(sumLeft == maxForward[maxInd+1]) left = i;
            }

            int min = 0;
            int changeInd = -1;
            for (int i = left; i <= right; i++)
            {
                if (A[i] < min)
                {
                    changeInd = i;
                    min = A[i];
                }
            }

            if (changeInd >= 0) maxSum -= A[changeInd];

            List<int> checkNumbers = new List<int>(N);
            {
                for(int i =left - 1; i>=0; i--)
                {
                    checkNumbers.Add(A[i]);
                }
            
                for(int i =right + 1; i<N; i++)
                {
                    checkNumbers.Add(A[i]);
                }
            }

            int maxSumChecked = maxSum;
            foreach(var c in checkNumbers)
            {
                maxSumChecked = Math.Max(maxSum + c, maxSum);
            }

            return Math.Max(maxSumChecked, maxSum);
        }
    }
}
