using NUnit.Framework;
using System;
using System.Collections.Generic;
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


    }
}
