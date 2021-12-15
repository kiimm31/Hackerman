using System;

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
        private Beverage Beverage;

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

    public abstract class Car
    {
        protected bool isSedan;
        protected string seats;

        public Car()
        {
        }

        public Car(bool isSedan, string seats)
        {
            this.isSedan = isSedan;
            this.seats = seats;
        }

        public bool getIsSedan()
        {
            return this.isSedan;
        }

        public string getSeats()
        {
            return this.seats;
        }

        abstract public string getMileage();

        public void printCar(string name)
        {
            Console.WriteLine("A {0} is{1} Sedan, is {2}-seater, and has a mileage of around {3}.",
            name,
            this.getIsSedan() ? "" : " not",
            this.getSeats(),
            this.getMileage());
        }
    }

    public class WagonR : Car
    {
        private readonly int _mileage;

        public override string getMileage()
        {
            return $"{_mileage} kmpl";
        }

        public WagonR(int mileage)
        {
            _mileage = mileage;
            base.isSedan = false;
            base.seats = 4.ToString();
        }
    }
}


