using System;
using System.Collections.Generic;
using System.Text;
using DesignPattern;
using NUnit.Framework;

namespace PracticeHackerRank
{
    class DesignPatternTest
    {
        [Test]
        public void DecoratorPattern()
        {
            DecoratorPattern implementation = new DecoratorPattern();

            implementation.CreateDrink();

            decimal drinkCost = implementation.getCost();

            Assert.AreEqual(drinkCost, 1);

            implementation.addCaramel();

            drinkCost = implementation.getCost();

            Assert.AreEqual(drinkCost, 2);
        }

        [Test]
        public void TemplatePattern()
        {
            TemplateMethodPattern templateMethodPattern = new TemplateMethodPattern();

            templateMethodPattern.Save();
        }

    }
}
