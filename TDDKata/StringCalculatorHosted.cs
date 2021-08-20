using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TDDKata
{
    /// <summary>
    /// https://osherove.com/tdd-kata-1
    /// </summary>
    public class StringCalculatorHosted
    {
        //1 create failing test     (1)(2)
        //2 make test pass      (2)
        //3. make test better   (2)
        [TestCase("0", ExpectedResult = 0)]
        [TestCase("1,2", ExpectedResult = 3)]
        [TestCase("2,2", ExpectedResult = 4)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("1\n2,3", ExpectedResult = 6)]
        [TestCase("//;\n1;2", ExpectedResult = 3)]
        public int AddTest(string numbers)
        {
            var result = Add(numbers);
            //Assert.AreEqual(result, 0);
            return result;
        }

        [TestCase("//;\n-1;2", "-1")]
        [TestCase("//;\n-2;2", "-2")]
        [TestCase("//;\n-2;-2", "-2,-2")]
        public void AddNegativeNumbers(string numbers, string expected)
        {
            var exception = Assert.Throws<Exception>(() => Add(numbers));
            Assert.AreEqual($"negatives not allowed, {expected}", exception.Message);
        }


        private int Add(string numbers)
        {
            if (string.IsNullOrWhiteSpace(numbers))
            {
                return 0;
            }

            var index = numbers.IndexOf('/') + 2;

            var delimiter = index == 1 ? ',' : numbers[index];

            numbers = index == 1 ? numbers : numbers.Substring(index + 2);

            var number = numbers.Replace('\n', delimiter).Split(delimiter);

            var result = 0;
            var invalidNumber = new List<string>();

            foreach (var numberStr in number)
            {
                if (int.Parse(numberStr) < 0)
                {
                    invalidNumber.Add(numberStr);
                }
                else
                {
                    result += int.Parse(numberStr);
                }
            }

            if (invalidNumber.Any())
            {
                throw new Exception($"negatives not allowed, {string.Join(",", invalidNumber)}");
            }

            return result;
        }
    }
}
