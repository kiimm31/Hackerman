using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace PracticeHackerRank
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConvertToList()
        {
            int[,] input = new int[3, 3]
                {{ 5, 3, 4 }, { 1, 5, 8 }, { 6, 4, 2 } };

            List<int[,]> vs1 = getAllPossibleMagicCube();

            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();

            foreach (int[,] cube in vs1)
            {
                int indexCube = vs1.IndexOf(cube);

                int noOfDifference = 0;
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        if (cube[row, col] != input[row, col])
                        {
                            noOfDifference += Math.Abs(input[row, col] - cube[row, col]);
                        }
                    }
                }

                keyValuePairs.Add(indexCube, noOfDifference);
            }

            var d = keyValuePairs.Min(x => x.Value);


            Assert.AreEqual(1, 1);
        }

        List<int[,]> getAllPossibleMagicCube()
        {
            List<int[,]> vs1 = new List<int[,]>()
            {
                new int[3,3] { { 8, 1, 6 }, { 3, 5, 7 }, { 4, 9, 2 } },
                new int[3,3] { { 6, 1, 8 }, { 7, 5, 3 }, { 2, 9, 4 } },
                new int[3,3] { { 4, 9, 2 }, { 3, 5, 7 }, { 8, 1, 6 } },
                new int[3,3] { { 2, 9, 4 }, { 7, 5, 3 }, { 6, 1, 8 } },
                new int[3,3] { { 8, 3, 4 }, { 1, 5, 9 }, { 6, 7, 2 } },
                new int[3,3] { { 4, 3, 8 }, { 9, 5, 1 }, { 2, 7, 6 } },
                new int[3,3] { { 6, 7, 2 }, { 1, 5, 9 }, { 8, 3, 4 } },
                new int[3,3] { { 2, 7, 6 }, { 9, 5, 1 }, { 4, 3, 8 } },
            };
            return vs1;
        }

        [Test]
        public void pickingNumbers()
        {
            List<int> a = new List<int>() { 4, 97, 5, 97, 97, 4, 97, 4, 97, 97, 97, 97, 4, 4, 5, 5, 97, 5, 97, 99, 4, 97, 5, 97, 97, 97, 5, 5, 97, 4, 5, 97, 97, 5, 97, 4, 97, 5, 4, 4, 97, 5, 5, 5, 4, 97, 97, 4, 97, 5, 4, 4, 97, 97, 97, 5, 5, 97, 4, 97, 97, 5, 4, 97, 97, 4, 97, 97, 97, 5, 4, 4, 97, 4, 4, 97, 5, 97, 97, 97, 97, 4, 97, 5, 97, 5, 4, 97, 4, 5, 97, 97, 5, 97, 5, 97, 5, 97, 97, 97 };

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

        [Test]
        public void alice()
        {

            int[] scores = { 100, 100, 50, 40, 40, 20, 10 };

            int[] alice = { 5, 25, 50, 120, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };

            List<int> returnList = new List<int>();

            List<int> ranks = scores.ToList().Distinct().OrderByDescending(x => x).ToList();

            Dictionary<int, int> rankDic = new Dictionary<int, int>();

            foreach (int aliceScore in alice.ToList())
            {
                if (rankDic.ContainsKey(aliceScore))
                {
                    returnList.Add(rankDic[aliceScore]);
                }
                else
                {
                    List<int> TempList = new List<int>();

                    TempList.AddRange(ranks);

                    TempList.Add(aliceScore);

                    int myRank = TempList.Distinct().OrderByDescending(x => x).ToList().IndexOf(aliceScore) + 1;

                    returnList.Add(myRank);

                    rankDic.Add(aliceScore, myRank);
                }
            }


            Assert.IsNotNull(returnList);
        }

        private int GetRank(int[] score, int v)
        {
            List<int> ranking = score.ToList();

            ranking.Add(v);

            ranking = ranking.OrderByDescending(x => x).ToList();

            int previousScore = 0;

            int rank = 1;
            foreach (int player in ranking)
            {
                if (player < previousScore)
                {
                    rank++;
                }
                if (player == v)
                {
                    return rank;
                }

                previousScore = player;

            }
            return 1;
        }

        [Test]
        public void timeInWords()
        {
            int h = 1;
            int m = 1;
            String returnString = String.Empty;


            string hour = String.Empty;
            string minute = string.Empty;
            if (m == 30)
            {
                // half past h

                if (h > 10)
                {
                    hour = tens(h.ToString());
                }
                else
                {
                    hour = ones(h.ToString());
                }

                returnString = $"Half Past {hour}";

            }
            else if (m == 0)
            {
                // hour o' clock

                if (h > 10)
                {
                    hour = tens(h.ToString());
                }
                else
                {
                    hour = ones(h.ToString());
                }

                returnString = $"{hour} o' clock";

            }

            else if (m > 30)
            {
                // 60 - m minutes to h + 1
                m = 60 - m;

                h = h + 1;

                if (h > 10)
                {
                    hour = tens(h.ToString());
                }
                else
                {
                    hour = ones(h.ToString());
                }

                if (m > 10)
                {
                    minute = tens(m.ToString());
                }
                else
                    minute = ones(m.ToString());

                returnString = $"{minute} minutes to {hour}";
            }
            else
            {
                // m past h

                if (h > 10)
                {
                    hour = tens(h.ToString());
                }
                else
                {
                    hour = ones(h.ToString());
                }

                if (m > 10)
                {
                    minute = tens(m.ToString());
                }
                else
                    minute = ones(m.ToString());

                returnString = $"{minute} minutes past {hour}";
            }
            if (m == 15)
            {
                returnString = returnString.Replace(" minutes", "");
            }


        }

        private static String ones(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = "";
            switch (_Number)
            {

                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
            return name;
        }

        private static String tens(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = null;
            switch (_Number)
            {
                case 10:
                    name = "Ten";
                    break;
                case 11:
                    name = "Eleven";
                    break;
                case 12:
                    name = "Twelve";
                    break;
                case 13:
                    name = "Thirteen";
                    break;
                case 14:
                    name = "Fourteen";
                    break;
                case 15:
                    name = "quarter";
                    break;
                case 16:
                    name = "Sixteen";
                    break;
                case 17:
                    name = "Seventeen";
                    break;
                case 18:
                    name = "Eighteen";
                    break;
                case 19:
                    name = "Nineteen";
                    break;
                case 20:
                    name = "Twenty";
                    break;
                case 30:
                    name = "Thirty";
                    break;
                case 40:
                    name = "Fourty";
                    break;
                case 50:
                    name = "Fifty";
                    break;
                case 60:
                    name = "Sixty";
                    break;
                case 70:
                    name = "Seventy";
                    break;
                case 80:
                    name = "Eighty";
                    break;
                case 90:
                    name = "Ninety";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }

        [Test]
        [TestCase(6, 2, 2, ExpectedResult = 5)]
        public int chocolateFeast(int n, int c, int m)
        {
            double wrappersOnHand = 0;

            double chocolateOnHand = n / c;

            double chocolateEaten = 0;

            while ((chocolateOnHand + wrappersOnHand) >= m)
            {
                //i can change for some chocolate
                //eat the chocolate
                wrappersOnHand += chocolateOnHand;
                chocolateEaten += chocolateOnHand;
                chocolateOnHand = 0;
                //change for more chocolate;
                double changed = Math.Floor(wrappersOnHand / m);
                wrappersOnHand -= m * changed;

                chocolateOnHand += changed;
            }
            chocolateEaten += chocolateOnHand;

            return Convert.ToInt32(chocolateEaten);
        }

        [Test]
        public void serviceLane()
        {

            int n = 8;
            int[][] cases = new int[][] {
                new int[]{0,3 },
                new int[]{4,6 },
                new int[]{6,7 },
                new int[]{3,5 },
                new int[]{0,7 }
            };


            int[] width = new int[] { 2, 3, 1, 2, 3, 2, 3, 3 };

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
        }

        [Test]
        [TestCase(25)]
        public void extraLongFactorials(int n)
        {
            BigInteger frac = new BigInteger(1);

            for (int i = 1; i <= n; i++)
            {
                frac = frac * i;
            }
        }

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


        [Test]
        [TestCase("haveaniceday", ExpectedResult = "hae and via ecy")]
        [TestCase("chillout", ExpectedResult = "clu hlt io")]
        public string encryption(string s)
        {
            s = s.Replace(" ", "");

            int row = (int)Math.Floor(Math.Sqrt(s.Length));
            int col = (int)Math.Ceiling(Math.Sqrt(s.Length));

            List<string> splitString = new List<string>();

            char[] charArray = s.ToCharArray();

            string commentSubstring = string.Empty;
            for (int i = 0; i < charArray.Length; i++)
            {
                commentSubstring += charArray[i];
                if (commentSubstring.Length == col)
                {
                    splitString.Add(commentSubstring);
                    commentSubstring = string.Empty;
                }
                else if (i == charArray.Length - 1)
                    splitString.Add(commentSubstring);
            }

            List<string> returnList = new List<string>();

            for (int i = 0; i < col; i++)
            {
                string myStr = string.Empty;

                foreach (string cols in splitString)
                {
                    if (cols.Length > i)
                    {
                        myStr += cols[i];
                    }
                }

                returnList.Add(myStr);
            }

            return string.Join(" ", returnList);
        }

        [Test]
        [TestCase("caberqiitefg", 5, ExpectedResult = "erqii")]
        [TestCase("azerdii", 5, ExpectedResult = "erdii")]
        [TestCase("qwdftr", 5, ExpectedResult = "Not found!")]
        public string fizzBuzz(string s, int k)
        {
            string returnString = "Not found!";

            List<char> Vowels = new List<char>()
            {
                'a','e','i','o','u'
            };

            List<Tuple<string, int>> subStrings = new List<Tuple<string, int>>();

            if (!s.Any(x => Vowels.Contains(x)))
            {
                return returnString;
            }

            for (int i = k; i <= s.Length; i++)
            {
                string substring = s.Substring(i - k, k);

                int totalVowels = 0;

                if (substring.Any(x => Vowels.Contains(x)))
                {
                    totalVowels = substring.Count(x => Vowels.Contains(x));
                }

                if (totalVowels > 0)
                {
                    if (totalVowels == k)
                    {
                        return substring;
                    }

                    subStrings.Add(new Tuple<string, int>(substring, totalVowels));
                }

                if (!s.Substring((i), (s.Length - i)).Any(x => Vowels.Contains(x)))
                {
                    break;
                }
            }

            subStrings = subStrings.OrderByDescending(x => x.Item2).ToList();

            returnString = subStrings?.FirstOrDefault()?.Item1 ?? returnString;

            return returnString;
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


        [Test]
        [TestCase(1, 3, 5, 7, 9, ExpectedResult = "BOB")]
        [TestCase(7, 4, 6, 5, 8, ExpectedResult = "ANDY")]
        public string gamingArray(params int[] arr1)
        {
            List<int> arr = arr1.ToList();

            char turn = 'B';

            var i = arr.OrderBy(x => x);

            if (arr.OrderBy(x => x).SequenceEqual(arr))
            {
                // all is inorder, dont need to play already know who will win depending on even odd

                int evenOdd = arr.Count() % 2;

                if (evenOdd == 1)
                {
                    return "BOB";
                }
                else
                {
                    return "ANDY";
                }
            }

            while (arr.Any())
            {
                int indexLargest = arr.IndexOf(arr.Max());

                int countLeft = arr.Count() - indexLargest;

                arr.RemoveRange(indexLargest, countLeft);

                turn = switchTurn(turn);
            }


            if (turn == 'B')
            {
                return "ANDY";
            }
            else
            {
                return "BOB";
            }
        }

        private char switchTurn(char turn)
        {
            if (turn == 'B')
            {
                return 'A';
            }
            else
            {
                return 'B';
            }
        }
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

        [Test]
        public void a()
        {
            List<int> firstDay = new List<int>() { 1, 10, 11 };
            List<int> lastDay = new List<int>() { 11, 10, 11 };


            List<Tuple<int, int>> totalInt = new List<Tuple<int, int>>();


            for (int investor = 0; investor < firstDay.Count(); investor++)
            {
                List<Tuple<int, int>> thisInvestorAvailableDay = new List<Tuple<int, int>>();

                for (int day = firstDay[investor]; day <= lastDay[investor]; day++)
                {
                    thisInvestorAvailableDay.Add(new Tuple<int, int>(day, investor));
                }

                totalInt.AddRange(thisInvestorAvailableDay);

            }

            int sm = firstDay.Min();

            int bg = lastDay.Max();

            bool[] schedule = new bool[bg];

            List<int> alreadyScheduled = new List<int>();

            totalInt = totalInt.OrderBy(x => x.Item2).ToList();

            foreach (var item in totalInt)
            {
                if (alreadyScheduled.Contains(item.Item2))
                {
                    continue;
                }
                else
                {
                    if (schedule[item.Item1 - 1] == true)
                    {
                        continue;
                    }
                    schedule[item.Item1 - 1] = true;
                    alreadyScheduled.Add(item.Item2);
                }
            }

            var returnInt = alreadyScheduled.Count(); ;

        }

        [Test]
        public void b()
        {
            List<int> numbers = new List<int>() { 1, 1, 2, 2, 3, 3, };

            int k = 1;

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

            var pairs = keyValuePairs.Count();


        }

        [Test]
        public void c()
        {
            List<string> c = new List<string>(); //name 
            List<int> x = new List<int>();
            List<int> y = new List<int>();
            List<string> q = new List<string>();

            List<string> returnList = new List<string>();

            foreach (string query in q)
            {
                int index = q.IndexOf(query);

                int closestCityIndex = 0;

                int myX = x[index];

                int myY = y[index];

                // do work

                // get all index where same X

                List<int> indexX = x.Where(y => y == myX).Select(o => x.IndexOf(o)).ToList();
                List<int> indexY = y.Where(x => x == myY).Select(o => y.IndexOf(o)).ToList();

                // cal Y diff with X index
                List<int> yDiff = new List<int>();

                y.ForEach(i =>
                {
                    yDiff.Add(Math.Abs(x[i] - myY));
                });

                List<int> xDiff = new List<int>();
                x.ForEach(j =>
                {
                    xDiff.Add(Math.Abs(y[j] - myX));
                });

                List<int> completeList = new List<int>();

                completeList.AddRange(yDiff);
                completeList.AddRange(xDiff);

                closestCityIndex = completeList.Min();


                returnList.Add(c[closestCityIndex]);
            }


        }
    }
}