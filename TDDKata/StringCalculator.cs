using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TDDKata
{
    /// <summary>
    /// https://osherove.com/tdd-kata-1
    /// </summary>
    public class StringCalculator
    {
        public int Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;

            var delimiter = ";";
            if (input.StartsWith("//") && input.Contains("\n"))
            {
                var strings = input.Split("\n").ToList();
                 delimiter = strings.First().Substring(2);
                strings.RemoveAt(0);
                input = string.Join(delimiter, strings);
            }

            var numbersStr = input.Replace("\n", delimiter).Split(delimiter);
            var numbers = numbersStr.Select(int.Parse);
            if (numbers.Any(x => x < 0))
            {
                throw new Exception("Negatives Not Allowed");
            }

            return numbers.Where(x => x <= 1000).Sum();
        }
    }

    public class StringCalculatorTest
    {
        private StringCalculator _strCalculator;

        [SetUp]
        public void Setup()
        {
            _strCalculator = new StringCalculator();
        }

        [TestCase("1;2;3", ExpectedResult = 6)]
        [TestCase("1", ExpectedResult = 1)]
        [TestCase("", ExpectedResult = 0)]
        public int GivenNumbersInString_ShouldBeAbleToAdd(string input)
        {
            return _strCalculator.Add(input);
        }

        [TestCase("1;2\n3", ExpectedResult = 6)]
        public int GivenNumberWithNewLine_ShouldBeAbleToAdd(string input)
        {
            return _strCalculator.Add(input);
        }

        [TestCase("//;\n1;2", ExpectedResult = 3)]
        public int GivenDelimiter_ShouldBeAbleToAdd(string input)
        {
            return _strCalculator.Add(input);
        }

        [TestCase("-1")]
        public void GivenNegativeNumber_ShouldThrowNegativeNotAllowed(string input)
        {
            Action adding = () => _strCalculator.Add(input);
            adding.Should().Throw<Exception>().Which.Message.Should().Be("Negatives Not Allowed");
        }

        [TestCase("1001;2", ExpectedResult = 2)]
        [TestCase("1000;2", ExpectedResult = 1002)]
        public int Given1001_ShouldIgnore(string input)
        {
            return _strCalculator.Add(input);
        }
    }
}