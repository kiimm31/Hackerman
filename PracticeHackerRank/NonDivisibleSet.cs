using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeHackerRank
{
    class NonDivisibleSet
    {
        [Test]
        [TestCase(3, 1, 7, 2, 4, ExpectedResult = 3)]
        [TestCase(9, 422346306, 940894801, 696810740, 862741861, 85835055, 313720373, ExpectedResult = 5)]
        [TestCase(7, 278, 576, 496, 727, 410, 124, 338, 149, 209, 702, 282, 718, 771, 575, 436, ExpectedResult = 11)]
        public int nonDivisibleSubSet(int k, params int[] input)
        {
            List<int> s = input.ToList();

            List<Tuple<int, int, int>> listOfSums = new List<Tuple<int, int, int>>();

            foreach (int value in s)
            {
                for (int i = 0; i < s.Count(); i++)
                {
                    if (s.IndexOf(value) != i && s.IndexOf(value) > i) // prevent double Count
                    {
                        listOfSums.Add(new Tuple<int, int, int>(value + s[i], s.IndexOf(value), i));
                    }
                }
            }

            List<int> returnList = new List<int>();

            foreach (Tuple<int, int, int> sum in listOfSums)
            {
                if (sum.Item1 % k != 0)
                {// not divisible
                    returnList.Add(sum.Item2);
                    returnList.Add(sum.Item3);
                }
            }

            var t = returnList.Distinct();

            return t.Count();
        }
    }
}
