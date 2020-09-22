using System;
using System.Collections.Generic;
using System.Text;
using DecoratorPattern;
using NUnit.Framework;

namespace PracticeHackerRank
{
    class DesignPatternTest
    {
        [Test]
        public void DecoratorPattern()
        {
            DecoratorPattern.DecoratorPattern implementation = new DecoratorPattern.DecoratorPattern();

            implementation.CreateDrink();

            decimal drinkCost = implementation.getCost();

            Assert.AreEqual(drinkCost, 1);

            implementation.addCaramel();

            drinkCost = implementation.getCost();

            Assert.AreEqual(drinkCost, 2);
        }

    }
}
