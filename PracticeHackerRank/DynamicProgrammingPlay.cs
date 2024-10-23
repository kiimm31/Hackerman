using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework.Internal.Execution;
using NUnit.Framework.Internal;

namespace PracticeHackerRank
{
    internal class DynamicProgramming
    {
        [SetUp]
        public void SetUp()
        {
            coinDb = new Dictionary<int, int>();
            _fibonacciCache = new Dictionary<long, long>();
        }

        #region fibonacci

        private Dictionary<long, long> _fibonacciCache = new Dictionary<long, long>();

        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 1)]
        [TestCase(7, ExpectedResult = 13)]
        [TestCase(50, ExpectedResult = 12586269025)]

        public long Fibonacci(long sequence)
        {
            if (_fibonacciCache.ContainsKey(sequence))
            {
                return _fibonacciCache[sequence];
            }

            if (sequence <= 2)
            {
                return 1;
            }

            else
            {
                var ans = Fibonacci(sequence - 1) + Fibonacci(sequence - 2);
                _fibonacciCache.Add(sequence, ans);
                return ans;
            }
        }

        #endregion

        #region min coin

        private Dictionary<int, int> coinDb = new Dictionary<int, int>();

        [TestCase(13, 1, 4, 5, ExpectedResult = 3)]
        [TestCase(3, 1, 2, ExpectedResult = 2)]
        [TestCase(150, 1, 4, 5, ExpectedResult = 30)]

        public int MinCoin(int target, params int[] coinChoices)
        {
            if (target == 0)
            {
                return 0;
            }
            else
            {
                if (coinDb.ContainsKey(target))
                {
                    return coinDb[target];
                }

                int ans = int.MaxValue;
                foreach (var item in coinChoices)
                {
                    var leftOver = target - item;
                    if (leftOver < 0)
                    {
                        continue;
                    }
                    ans = Math.Min(MinCoin(leftOver, coinChoices) + 1, ans);
                }

                coinDb.Add(target, ans);
                return ans;
            }
        }

        #endregion

    }
}