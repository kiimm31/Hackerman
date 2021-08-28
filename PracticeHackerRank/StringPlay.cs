using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticeHackerRank
{
    internal class StringPlay
    {
        #region TimeWords

        [Test]
        [TestCase(1, 1, ExpectedResult = "ONE MINUTE PAST ONE")]
        public string timeInWords(int h, int m)
        {
            string returnString = string.Empty;

            string hour = string.Empty;
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
            else if (m == 1)
            {
                returnString = returnString.Replace("minutes", "minute");
            }

            return returnString.ToUpper();
        }

        private static string ones(string Number)
        {
            int _Number = Convert.ToInt32(Number);
            string name = "";
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

        private static string tens(string Number)
        {
            int _Number = Convert.ToInt32(Number);
            string name = null;
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

        #endregion TimeWords

        #region VowelCount

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

        #endregion VowelCount

        #region Encryption

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

        #endregion Encryption

        #region GridSearch

        [Test]
        [TestCaseSource(nameof(gridSearchCases))]
        public void gridSearch(string[] G, string[] P, string expectResult)
        {
            string returnString = "NO";

            for (int gFirst = 0; gFirst < G.Length; gFirst++)
            {
                int gIndex = gFirst;
                List<List<int>> listOfIndex = new List<List<int>>();

                for (int pIndex = 0; pIndex < P.Length; pIndex++)
                {
                    string controlString = G[gIndex];
                    if (controlString.Contains(P[pIndex]) && gIndex < G.Length)
                    {
                        listOfIndex.Add(AllIndexesOf(G[gIndex], P[pIndex]));
                        gIndex++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (listOfIndex.Count() == P.Length)
                {
                    // ascending row contains P
                    // check if all list contains a same number
                    List<int> commonList = FindCommon<int>(listOfIndex);

                    if (commonList?.Any() ?? false)
                    {
                        returnString = "YES";
                        break;
                    }
                }
            }

            Assert.AreEqual(returnString, expectResult);
        }

        public List<T> FindCommon<T>(IEnumerable<List<T>> Lists)
        {
            return Lists.SelectMany(x => x).Distinct()
                .Where(x => Lists.Select(y => (y.Contains(x) ? 1 : 0))
                .Sum() == Lists.Count()).ToList();
        }

        public List<int> AllIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; index <= str.Length - value.Length; index++)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
            return indexes;
        }

        private static object[] gridSearchCases =
        {
            new object[]
            {
                 new string[]
                 {
                     "7283455864",
                     "6731158619",
                     "8988242643",
                     "3830589324",
                     "2229505813",
                     "5633845374",
                     "6473530293",
                     "7053106601",
                     "0834282956",
                     "4607924137"
                 },
                 new string[]
                 {
                     "9505",
                     "3845",
                     "3530"
                 },
                 "YES"
            },

            new object[]
            {
                new string[]
                {
                    "123412",
                    "561212",
                    "123634",
                    "781288"
                },
                new string[]
                {
                    "12",
                    "34"
                }
                , "YES"
            },
            new object[]
            {
                new string[]
                {
                    "111111111111111",
                    "111111111111111",
                    "111111011111111",
                    "111111111111111",
                    "111111111111111"
                },
                new string[]
                {
                    "11111",
                    "11111",
                    "11110"
                },
                "YES"
            },
        };

        #endregion GridSearch

        public static void fizzBuzz(int n)
        {
            for (int integer = 1; integer <= n; integer++)
            {
                string returnString = string.Empty;

                if (integer % 3 == 0)
                {
                    //can divide by 3
                    returnString += "Fizz";
                }
                if (integer % 5 == 0)
                {
                    //can divide by 5
                    returnString += "Buzz";
                }

                if (string.IsNullOrWhiteSpace(returnString))
                {
                    Console.WriteLine(integer.ToString());
                }
                else
                {
                    Console.WriteLine(returnString);
                }
            }
        }

        [Test]
        //[TestCase("[]([][)[[)[((])(((]][()(()))])][[)]([]])", ExpectedResult = 1)]
        //[TestCase("[(?][??[", ExpectedResult = 2)]
        [TestCase("?)(])?", ExpectedResult = 2)]
        public int fillMissingBrackets(string s)
        {
            int numberOfRound = 0;
            int numberOfSquare = 0;
            int numberofUnknown = 0;

            int counter = 0;

            foreach (char item in s)
            {
                switch (item)
                {
                    case '(':
                        numberOfRound++;
                        break;

                    case ')':
                        numberOfRound--;
                        break;

                    case '[':
                        numberOfSquare++;
                        break;

                    case ']':
                        numberOfSquare--;
                        break;

                    case '?':
                        numberofUnknown++;
                        break;

                    default:
                        break;
                }

                if ((numberOfRound - numberofUnknown) <= 0)
                {
                    int tempUnknownLeft = (numberofUnknown - Math.Abs(numberOfRound));
                    if (numberOfSquare - tempUnknownLeft == 0)
                    {
                        counter++;
                        numberofUnknown = 0;
                    }
                }
            }

            if (counter > 2 && counter > 0)
            {
                return counter > 2 ? (int)Math.Ceiling((double)counter / 2) : 1;
            }
            else
                return 0;
        }

        [Test]
        [TestCase("RBY_YBR", ExpectedResult = "YES")]
        [TestCase("X_Y__X", ExpectedResult = "NO")]
        [TestCase("__", ExpectedResult = "YES")]
        [TestCase("B_RRBR", ExpectedResult = "YES")]
        [TestCase("AABBC", ExpectedResult = "NO")]
        [TestCase("AABBC_C", ExpectedResult = "YES")]
        [TestCase("DD__FQ_QQF", ExpectedResult = "YES")]
        [TestCase("AABCBC", ExpectedResult = "NO")]
        public string happyLadybugs(string s)
        {
            if (s.Contains("_"))
            {// only needs to know if every letter more than 2
                IEnumerable<IGrouping<char, char>> letters = s.Replace("_", "").GroupBy(x => x);

                if (letters.Any(x => x.Count() <= 1))
                {
                    return "NO";
                }
            }
            else
            {// need to know if every letter is next to itself
                List<Tuple<char, int>> tuple = new List<Tuple<char, int>>();
                foreach (char _char in s)
                {
                    if (tuple.LastOrDefault()?.Item1 == _char)
                    {
                        tuple[tuple.Count() - 1] = new Tuple<char, int>(_char, tuple.LastOrDefault().Item2 + 1);
                    }
                    else
                    {// previously already have some char not next to each other
                        tuple.Add(new Tuple<char, int>(_char, 1));
                    }
                }

                if (tuple.Any(x => x.Item2 <= 1))
                {
                    return "NO";
                }
            }

            return "YES";
        }
    }
}