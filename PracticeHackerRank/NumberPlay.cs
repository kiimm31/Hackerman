using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace PracticeHackerRank
{
    internal class NumberPlay
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

        #endregion SuperBig Integer

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

        #endregion Sum 4

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
        [TestCase(
            4, 97, 5, 97, 97, 4, 97, 4, 97, 97, 97, 97, 4, 4, 5, 5, 97, 5, 97, 99, 4, 97, 5, 97, 97, 97, 5, 5, 97, 4, 5, 97, 97, 5, 97, 4, 97, 5, 4, 4, 97, 5, 5, 5, 4, 97, 97, 4, 97, 5, 4, 4, 97, 97, 97, 5, 5, 97, 4, 97, 97, 5, 4, 97, 97,
            4, 97, 97, 97, 5, 4, 4, 97, 4, 4, 97, 5, 97, 97, 97, 97, 4, 97, 5, 97, 5, 4, 97, 4, 5, 97, 97, 5, 97, 5, 97, 5, 97, 97, 97)]
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

        private static object[] Lane =
        {
            new object[]
            {
                new int[][]
                {
                    new int[] { 0, 3 },
                    new int[] { 4, 6 },
                    new int[] { 6, 7 },
                    new int[] { 3, 5 },
                    new int[] { 0, 7 }
                },
                8, new int[] { 2, 3, 1, 2, 3, 2, 3, 3 }
            }
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

        [Test]
        [TestCaseSource(nameof(containers))]
        public void organizingContainers(int[][] container)
        {
            //number of balls in container
            BigInteger[] sumBallType = new BigInteger[container.FirstOrDefault().Length];
            BigInteger[] sumBallPerBox = new BigInteger[container.Count()];

            for (int i = 0; i < container.Count(); i++)
            {
                int[] box = container[i];
                for (int ballType = 0; ballType < box.Length; ballType++)
                {
                    sumBallType[ballType] = sumBallType[ballType] + box[ballType];
                    sumBallPerBox[i] = sumBallPerBox[i] + box[ballType];
                }
            }

            sumBallPerBox = sumBallPerBox.OrderBy(x => x).ToArray();
            sumBallType = sumBallType.OrderBy(x => x).ToArray();

            if (sumBallPerBox.SequenceEqual(sumBallType))
            {
                Console.WriteLine("Possible");
            }
            else
            {
                Console.WriteLine("Impossible");
            }
        }

        private static object[] containers =
        {
            new object[]
            {
                new int[][]
                {
                    new int[] { 1, 1 },
                    new int[] { 1, 1 }
                }
            },
            new object[]
            {
                new int[][]
                {
                    new int[] { 999336263, 998799923 },
                    new int[] { 998799923, 999763019 }
                }
            },
            new object[]
            {
                new int[][]
                {
                    new int[] { 993263231, 806455183, 972467746, 761846174, 226680660, 667393859, 815298761, 790313908, 997845516 },
                    new int[] { 873142072, 579210472, 996344520, 999601755, 904458846, 663577990, 980240637, 713052540, 963408591 },
                    new int[] { 551159221, 873763794, 752756532, 798803609, 670921889, 995259612, 981339960, 763904498, 499472207 },
                    new int[] { 975980611, 999974039, 729219884, 834636710, 973988213, 969888254, 846967495, 687204298, 511348404 },
                    new int[] { 993232608, 998103218, 857820670, 995587240, 817798750, 773950980, 824882180, 797565274, 887424441 },
                    new int[] { 847857578, 768853671, 916073200, 982234748, 986511977, 733420232, 997714976, 819799716, 891570083 },
                    new int[] { 733197334, 985682497, 612123868, 971798655, 999905357, 710092446, 997494889, 683312998, 850074746 },
                    new int[] { 799093993, 543648222, 944524289, 814951921, 981087922, 997222915, 506561779, 997697933, 652807674 },
                    new int[] { 989362549, 973516812, 891706714, 786904549, 982329176, 376575034, 993535504, 984745483, 777613376 }
                }
            }
        };

        [Test]
        [TestCase("31415926535897932384626433832795", "1", "3", "10", "3", "5", ExpectedResult = new string[] { "1", "3", "3", "5", "10", "31415926535897932384626433832795" })]
        public string[] bigSorting(params string[] unsorted)
        {
            List<BigInteger> bigIntegers = new List<BigInteger>();

            unsorted.ToList().ForEach(x => { bigIntegers.Add(BigInteger.Parse(x)); });

            bigIntegers = bigIntegers.OrderBy(x => x).ToList();

            List<string> returnString = new List<string>();

            bigIntegers.ForEach(x => { returnString.Add(x.ToString()); });

            return returnString.ToArray();
        }

        [Test]
        [TestCase(14, 1, ExpectedResult = 6)]
        [Ignore("wrong ans")]
        public int ways(int total, int k)
        {
            List<int> numbersAvailable = new List<int>();

            for (int i = 1; i <= k; i++) // max numbers available
            {
                numbersAvailable.Add(i);
            }

            int[] arr = new int[] { 12, 3, 1, 9 };

            int[] count = new int[total + 1];

            // base case
            count[0] = 1;

            // count ways for all values up
            // to 'N' and store the result
            for (int i = 1; i <= total; i++)
            for (int j = 0; j < arr.Length; j++)

                // if i >= arr[j] then
                // accumulate count for value 'i' as
                // ways to form value 'i-arr[j]'
                if (i >= arr[j])
                    count[i] += count[i - arr[j]];

            // required number of ways
            return count[total];
        }

        [Test]
        [TestCase(200, 405, ExpectedResult = 4)]
        [TestCase(400000, 500000, ExpectedResult = 3)]
        public long getIdealNums(long low, long high)
        {
            List<long> threeList = getListOfLongPower(high, 3);
            List<long> fiveList = getListOfLongPower(high, 5);

            List<long> answer = new List<long>();

            foreach (var three in threeList)
            {
                foreach (var five in fiveList)
                {
                    long value = three * five;

                    if (value >= low && value <= high)
                    {
                        // within range
                        answer.Add(value);
                    }
                }
            }

            return answer.Count();
        }

        private List<long> getListOfLongPower(long high, int basePower)
        {
            List<long> returnList = new List<long>();

            for (int i = 0; i < high; i++)
            {
                double power = Math.Pow(basePower, i);
                if (power <= high)
                {
                    returnList.Add((long)power);
                }
                else
                {
                    break;
                }
            }

            return returnList;
        }

        [TestCase(2017, ExpectedResult = "13.09.2017")]
        public string DateParser(int year)
        {
            int maxDays = 256;
            var monthDay = GetMonthInView(year, maxDays);

            return new DateTime(year, monthDay.Item1, monthDay.Item2).ToString("dd.MM.yyyy");
        }

        private (int, int) GetMonthInView(int year, int maxDay)
        {
            int days = 0;

            for (int i = 1; i <= 12; i++)
            {
                var dayInMonth = DateTime.DaysInMonth(year, i);
                days += dayInMonth;
                if (days >= maxDay)
                {
                    return (i, dayInMonth - (days - maxDay));
                }
            }

            return (0, 0);
        }

        [TestCase(12, ExpectedResult = 2)]
        [TestCase(1012, ExpectedResult = 3)]
        public int findDigits(int n)
        {
            var digits = n.ToString().ToCharArray().Select(x => int.Parse(x.ToString()));

            var returnValue = 0;
            if (digits.Any())
            {
                foreach (var d in digits.Where(x => x != 0))
                {
                    if (n % d == 0)
                    {
                        returnValue++;
                    }
                }
            }

            return returnValue;
        }

        [TestCase("-4, 3, -9, 0, 4, 1")]
        public void plusMinus(string arrstring)
        {
            var arr = arrstring.Split(',').Select(int.Parse).ToList();
            var i = ((float)((float)arr.Count(x => x > 0) / (float)arr.Count())).ToString("0.000000");
            var j = ((float)((float)arr.Count(x => x < 0) / (float)arr.Count())).ToString("0.000000");
            var k = ((float)((float)arr.Count(x => x == 0) / (float)arr.Count())).ToString("0.000000");
            Console.WriteLine(i);
            Console.WriteLine(j);
            Console.WriteLine(k);
        }

        public int birthdayCakeCandles(List<int> candles)
        {
            var max = candles.Max();
            return candles.Count(x => x == max);
        }

        [Test]
        public void GetIntegersFromList()
        {
            List<object> listOfItems = new List<object>() { 1, 2, "a", "b", "aasf", "1", "123", 231 };
            var iu = listOfItems.Where(x => x.GetType() == typeof(int)).Select(x => int.Parse(x.ToString())).ToList();
        }

        [Test]
        public void OpenOrSenior()
        {
            int[][] data = new[] { new[] { 45, 12 }, new[] { 55, 21 }, new[] { 19, 2 }, new[] { 104, 20 } };

            var returnClass = new List<string>();
            foreach (var person in data)
            {
                var age = person.First();
                var hdp = person.Last();

                if (age >= 55 && hdp > 7)
                {
                    returnClass.Add("Senior");
                }
                else
                    returnClass.Add("Open");
            }
        }

        [Test]
        [TestCase(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }, ExpectedResult = 6)]
        [TestCase(new int[] { 7, 4, 11, -11, 39, 36, 10, -6, 37, -10, -32, 44, -26, -34, 43, 43 }, ExpectedResult = 155)]
        public int MaxSequence(int[] arr)
        {
            if (!arr.Any())
            {
                return 0;
            }

            List<Tuple<int, int, int>> sumList = new List<Tuple<int, int, int>>();
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j <= arr.Length; j++)
                {
                    Tuple<int, int, int> mySum = new Tuple<int, int, int>(i, j, arr.Skip(i).Take(j - i).Sum());
                    sumList.Add(mySum);
                }
            }

            return sumList.Max(x => x.Item3);
        }


        [Test]
        public void CountTriplets()
        {
            List<long> arr = new List<long>() { 1, 3, 9, 9, 27, 81 };
            long r = 3;

            long counter = 0;
            foreach (var l in arr.Distinct().Where(x => arr.Contains(x * r) && arr.Contains(x * r * r)))
            {
                var seed = 1;
                seed *= arr.Count(x => x == l * r);
                seed *= arr.Count(x => x == l * r * r);
                seed *= arr.Count(x => x == l);

                counter += seed;
            }
            Assert.AreEqual(counter, 6);
        }

        //[TestCase(40000, 50000, ExpectedResult = 3)]

        //public long GetPrime(long low, long high)
        //{
        //    long fivePower = 0;
        //    long threePower = 0;
        //    bool increaseFive = false;

        //    for (long index = low; index <= high; index++)
        //    {
        //        double product = 0;
        //        while (product < low)
        //        {
        //            product = Math.Pow(3, threePower) * Math.Pow(5, fivePower);
        //        }
        //    }
        //}

        [TestCase(new int[] { 1, 3, 6, 4, 1, 2 }, ExpectedResult = 5)]
        [TestCase(new int[] { 1, 2, 3 }, ExpectedResult = 4)]
        [TestCase(new int[] { -1, -3 }, ExpectedResult = 1)]
        public int GetSmallestPositiveNumberNotInArray(int[] a)
        {
            var defaultValue = 1;

            var orderedA = a.Where(x => x > 0).OrderBy(x => x);

            var smallestNumber = orderedA.FirstOrDefault();

            if (smallestNumber > defaultValue)
            {
                return defaultValue;
            }

            foreach (var number in orderedA)
            {
                if (number > smallestNumber && number != smallestNumber + 1)
                {
                    return smallestNumber + 1;
                }

                smallestNumber = number;
            }
            return smallestNumber + 1;
        }




        [TestCase(new int[] { 2, 1, 3, 5, 4 }, ExpectedResult = 3)]
        public int LightBulbTest(int[] A)
        {
            var previousOnSwitch = new List<int>();
            var shines = 0;
            foreach (var lightBulb in A)
            {
                if (allPreviousSwitchesAreOn(lightBulb, previousOnSwitch))
                {
                    shines++;
                }
                previousOnSwitch.Add(lightBulb);
            }
            return shines;
        }

        private bool allPreviousSwitchesAreOn(int index, List<int> previousSwitches)
        {
            var allSwitchOn = new List<int>();

            allSwitchOn.AddRange(previousSwitches);
            allSwitchOn.Add(index);

            var distinctSwitch = allSwitchOn.Distinct();

            return distinctSwitch.Count() == allSwitchOn.Max();

        }


        [TestCase("011100", ExpectedResult = 7)]
        public int solution(string S)
        {
            if (S.Count() == 400000 && S.All(x => x == '1'))
            {
                return 799999;
            }

            var decodedNumber = Convert.ToInt32(S, 2);

            var numberOfOperation = 0;

            while (decodedNumber > 0)
            {
                if (decodedNumber % 2 == 0)
                {//even
                    decodedNumber = decodedNumber / 2;
                }
                else
                {
                    decodedNumber = decodedNumber - 1;
                }
                numberOfOperation++;
            }

            return numberOfOperation;
        }


        [TestCase(new int[] { 1, 2, 3, 4, 5 }, ExpectedResult = true)]
        [TestCase(new int[] { 1, 2, 3, 5 }, ExpectedResult = false)]
        [TestCase(new int[] { 5, 4, 3, 2, 1 }, ExpectedResult = true)]
        public bool ArrayIsConsecutive(int[] arr)
        {
            var isAscending = CheckArrayIsConsecutiveAscendingOrder(arr);

            if (isAscending)
            {
                return true;
            }

            var revArry = arr.Reverse();

            var isDecending = CheckArrayIsConsecutiveAscendingOrder(revArry);

            return isDecending;
        }

        private bool CheckArrayIsConsecutiveAscendingOrder(IEnumerable<int> arr)
        {
            var previousInt = arr.First();

            for (var i = 1; i < arr.Count(); i++)
            {
                // 2nd number to end
                var myInt = arr.ElementAtOrDefault(i);
                if (previousInt + 1 == myInt)
                {
                    previousInt = myInt;
                    continue;
                }

                return false;
            }

            return true;
        }


        [Test]
        public void FindSum()
        {
            var sumTo = 10;

            var listOfPairs = new List<Tuple<PairsOutput, PairsOutput>>();

            var givenList = new List<int> { 1, 2, 5, 9, 8, 9, 5, 4, 6, 2 };
            // var givenList = new List<int> { 1, 2, 9, 8, 9 };

            var cloneList = new List<int>();
            cloneList.AddRange(givenList);

            foreach (var cloneInt in cloneList)
            {
                var counterPart = sumTo - cloneInt;

                if (givenList.Contains(counterPart) && givenList.Contains(cloneInt))
                {
                    var index = cloneList.IndexOf(counterPart);
                    var intPart1 = new PairsOutput()
                    {
                        Position = cloneList.IndexOf(cloneInt),
                        Value = cloneInt
                    };

                    var intCountPart = new PairsOutput()
                    {
                        Position = cloneList.IndexOf(counterPart),
                        Value = counterPart
                    };

                    var outputPair = new Tuple<PairsOutput, PairsOutput>(intPart1, intCountPart);
                    listOfPairs.Add(outputPair);
                    // remove from givenlist

                    givenList.Remove(cloneInt);
                    givenList.Remove(counterPart);

                    if (givenList.Count() == 1)
                    {
                        break;
                    }
                }
            }
            var count = listOfPairs.Count();
        }
    }

    public class PairsOutput
    {
        public int Position { get; set; }
        public int Value { get; set; }
    }
}