using NUnit.Framework;
using System;

namespace PracticeHackerRank
{
    internal class ChocolateWrapperCount
    {
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
    }
}