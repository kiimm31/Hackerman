using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace PracticeHackerRank
{
    class NumberPlay
    {
        #region SuperBig Integer
        [Test]
        [TestCase(25)]
        public void extraLongFactorials(int n)
        {
            BigInteger frac = new BigInteger(1);

            for (int i = 1; i <= n; i++)
            {
                frac = frac * i;
            }

            Assert.IsNotNull(frac);
        }

        [Test]
        public void Sum()
        {
            List<int> n = new List<int>();

            BigInteger sum = new BigInteger(0);

            foreach (int o in n)
            {
                sum += new BigInteger(o);
            }

            long i = (long)(sum - sum);
        }

        #endregion

        #region Sum 4
        [Test]
        [TestCase(1, 2, 3, 4, 5, ExpectedResult = "10 14")]
        [TestCase(256741038, 623958417, 467905213, 714532089, 938071625, ExpectedResult = "2063136757 2744467344")]
        public string a(params int[] arr)
        {
            //Max

            List<ulong> tempMax = arr.OrderBy(x => x).Select(x => Convert.ToUInt64(x)).ToList();

            tempMax.RemoveAt(0);

            ulong Max = tempMax.Aggregate((a, c) => a + c);

            //min

            List<ulong> tempMin = arr.OrderByDescending(x => x).Select(x => Convert.ToUInt64(x)).ToList();

            tempMin.RemoveAt(0);

            ulong min = tempMin.Aggregate((a, c) => a + c);

            return $"{min} {Max}";

        }
        #endregion

        [Test]
        [TestCase(1, 1, 1, 2, 2, 3, 3, ExpectedResult = 2)]
        public int CountingPairs(int k, params int[] numbers)
        {
            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();

            for (int i = 1; i < numbers.Count(); i++)
            {
                for (int j = 0; j < numbers.Count(); j++)
                {
                    int secondIndex = numbers[i];
                    int firstIndex = numbers[j];

                    if (firstIndex + k == secondIndex)
                    {
                        // valid

                        KeyValuePair<int, int> kvp1 = new KeyValuePair<int, int>(firstIndex, secondIndex);

                        KeyValuePair<int, int> kvp2 = new KeyValuePair<int, int>(secondIndex, firstIndex);

                        if (!keyValuePairs.Contains(kvp1) && !keyValuePairs.Contains(kvp2))
                        {
                            keyValuePairs.Add(kvp1.Key, kvp1.Value);
                        }
                    }
                }
            }

            return keyValuePairs.Count();
        }

        [Test]
        [TestCase(4, 97, 5, 97, 97, 4, 97, 4, 97, 97, 97, 97, 4, 4, 5, 5, 97, 5, 97, 99, 4, 97, 5, 97, 97, 97, 5, 5, 97, 4, 5, 97, 97, 5, 97, 4, 97, 5, 4, 4, 97, 5, 5, 5, 4, 97, 97, 4, 97, 5, 4, 4, 97, 97, 97, 5, 5, 97, 4, 97, 97, 5, 4, 97, 97, 4, 97, 97, 97, 5, 4, 4, 97, 4, 4, 97, 5, 97, 97, 97, 97, 4, 97, 5, 97, 5, 4, 97, 4, 5, 97, 97, 5, 97, 5, 97, 5, 97, 97, 97)]
        public void pickingNumbers(params int[] a)
        {
            var count = a
    .Distinct()
    .Select(o => new { Value = o, Count = a.Count(c => c == o) })
    .OrderBy(o => o.Value);

            int previousValue = count.FirstOrDefault().Value;
            int previousCount = count.FirstOrDefault().Count;

            int maxCount = 0;

            for (int i = 1; i < count.Count(); i++)
            {
                if (Math.Abs(count.ElementAt(i).Value - previousValue) == 1)
                {
                    if (maxCount < (count.ElementAt(i).Count + previousCount))
                    {
                        maxCount = (count.ElementAt(i).Count + previousCount);
                    }
                }
                else
                {
                    if (maxCount < count.ElementAt(i).Count)
                    {
                        maxCount = count.ElementAt(i).Count;
                    }
                }

                previousCount = count.ElementAt(i).Count;
                previousValue = count.ElementAt(i).Value;
            }

            Assert.IsTrue(maxCount > 0);
        }

        static object[] Lane =
        {
            new object[] { new int[][] {
                new int[]{0,3 },
                new int[]{4,6 },
                new int[]{6,7 },
                new int[]{3,5 },
                new int[]{0,7 }
            }, 8, new int[] { 2, 3, 1, 2, 3, 2, 3, 3 }  }
        };

        [Test, TestCaseSource(nameof(Lane))]
        public void serviceLane(int[][] cases, int n, int[] width)
        {
            List<int> returnList = new List<int>();
            for (int i = 0; i < cases.Length; i++)
            {
                int entry = cases[i][0];
                int exit = cases[i][1];

                int minValue = width[entry];
                for (int j = entry; j <= exit; j++)
                {
                    if (minValue > width[j])
                    {
                        minValue = width[j];
                    }
                }

                returnList.Add(minValue);
            }
            Assert.IsNotNull(returnList);
        }

    }
}
