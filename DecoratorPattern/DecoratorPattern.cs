using System;
using System.Runtime.CompilerServices;

namespace DesignPattern
{
    public abstract class Beverage
    {
        public abstract decimal GetCost();
    }

    public abstract class AddonDecorator : Beverage
    {

    }

    public class Espresso : Beverage
    {
        public override decimal GetCost()
        {
            return 1;
        }
    }


    public class CaramelDecorator : AddonDecorator
    {
        private readonly Beverage _beverage;

        public CaramelDecorator(Beverage beverage)
        {
            _beverage = beverage;
        }

        public override decimal GetCost()
        {
            return this._beverage.GetCost() + 1;
        }
    }

    public class DecoratorPattern
    {
        Beverage Beverage;
        public DecoratorPattern()
        {

        }

        public void CreateDrink()
        {
            Beverage = new Espresso();
        }

        public void addCaramel()
        {
            Beverage = new CaramelDecorator(Beverage);
        }

        public decimal getCost()
        {
            return Beverage.GetCost();
        }
    }
}
