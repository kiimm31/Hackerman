using NUnit.Framework;

namespace PracticeHackerRank.Titansoft
{
    public class Stocks
    {
        [TestCase(18, 1, 10, 4, 9, 15, 13, ExpectedResult = 20)]
        [TestCase(18, 9, 10, 4, 15, 1, 13, ExpectedResult = 24)]
        [TestCase(9, 8, 9, 10, ExpectedResult = 2)]
        [TestCase(1, 2, 3, 4, 5, ExpectedResult = 4)]

        public int GetMaxProfit(params int[] prices)
        {
            int maxProfit = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                // If the price today is greater than the price yesterday, we can make a profit
                if (prices[i] > prices[i - 1])
                {
                    maxProfit += prices[i] - prices[i - 1];
                }
            }

            return maxProfit;
        }

    }
}
