using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeHackerRank
{
    class StringPlay
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

        #endregion


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
        #endregion

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
        #endregion


    }
}
