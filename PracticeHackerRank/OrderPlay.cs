using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PracticeHackerRank
{
    class OrderPlay
    {
        #region Cards
        [Test]
        public void Cards()
        {
            int[] x = new int[] { 0, 2, 3, 0 };

            int maxValue = x.Length;

            List<int> start = new List<int>();

            for (int j = 1; j <= maxValue; j++)
            {
                start.Add(j);
            }

            List<int[]> permutations = new List<int[]>();

            prnPermut(start.ToArray(), 0, maxValue - 1, ref permutations);

            permutations = permutations.OrderBy(x => x.Max(y => y)).ToList();

            List<int[]> matches = permutations.Where(y =>
            {
                bool isSame = true;
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] > 0)
                    {
                        if (y[i] != x[i])
                        {
                            isSame = false;
                        }
                    }
                }
                return isSame;
            }).ToList();

            int value = 0;

            foreach (int[] match in matches)
            {
                value += permutations.IndexOf(match);
            }
        }

        private void swapTwoNumber(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        private void prnPermut(int[] list, int k, int m, ref List<int[]> output)
        {
            int i;
            if (k == m)
            {
                List<int> perm = new List<int>();
                for (i = 0; i <= m; i++)
                {
                    perm.Add(list[i]);
                }
                output.Add(perm.ToArray());
            }
            else
                for (i = k; i <= m; i++)
                {
                    swapTwoNumber(ref list[k], ref list[i]);
                    prnPermut(list, k + 1, m, ref output);
                    swapTwoNumber(ref list[k], ref list[i]);
                }
        }

        #endregion

        #region SwapArray

        [Test]
        [TestCase(3, 1, 2, ExpectedResult = "YES")]
        [TestCase(1, 3, 4, 2, ExpectedResult = "YES")]
        [TestCase(1, 2, 3, 5, 4, ExpectedResult = "NO")]
        public string larrysArray(params int[] A)
        {
            List<int> previousValue = new List<int>();

            int inversion = 0;

            for (int i = 0; i < A.Length; i++)
            {
                inversion = inversion + previousValue.Count(x => x > A[i]);
                previousValue.Add(A[i]);
            }

            if (inversion % 2 == 0)
            {
                return "YES";
            }

            return "NO";
        }

        #endregion


        #region AlmostSored

        [Test]
        [TestCase(4, 2, ExpectedResult = "yes")]
        [TestCase(1, 5, 4, 3, 2, 6, ExpectedResult = "yes")]
        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 11, ExpectedResult = "yes")]
        public string almostSorted(params int[] arr)
        {
            int fromFront = 0;

            int fromBack = 0;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1])
                {
                    fromFront = i;
                    break;
                }
            }

            int z = -1;
  
            for (int j = arr.Length - 1; j > 0; j--)
            {
                z++;
                // 
                if (arr[j] < arr[j - 1])
                {
                    fromBack = z;
                    break;
                }
            }

            List<int> middleList = arr.Skip(fromFront).Take(arr.Length - fromFront - fromBack).ToList();

            if (middleList?.Any() ?? false)
            {
                if (middleList.SequenceEqual(middleList.OrderBy(x => x)))
                {
                    //swap
                    return "yes";
                }
                else if (middleList.SequenceEqual(middleList.OrderByDescending(x => x)))
                {
                    //reverse
                    return "yes";
                }
            }

            return "no";
        }


        #endregion


    }
}
