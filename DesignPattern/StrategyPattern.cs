using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public class Duck
    {
        private readonly IFlyBehaviour _flyBehaviour;
        private readonly IQuackBehaviour _quackBehaviour;

        public Duck(IFlyBehaviour flyBehaviour, IQuackBehaviour quackBehaviour)
        {
            _flyBehaviour = flyBehaviour;
            _quackBehaviour = quackBehaviour;
        }

        public string Quack()
        {
            return _quackBehaviour.Quack();
        }

        public string Fly()
        {
            return _flyBehaviour.Fly();
        }

    }

    public interface IQuackBehaviour
    {
        string Quack();
    }

    public interface IFlyBehaviour
    {
        string Fly();
    }


    public class JetFlyBehaviour : IFlyBehaviour
    {
        public string Fly()
        {
            return "Weeeeee";
        }
    }

    public class NoQuackBehaviour : IQuackBehaviour
    {
        public string Quack()
        {
            return "";
        }
    }

    public class StrategyPattern
    {
        Duck _duck;
        public StrategyPattern()
        {

        }

        public void GiveBirthToToyDuck()
        {
            _duck = new Duck(new JetFlyBehaviour(), new NoQuackBehaviour());
        }

        public string Quack()
        {
            return _duck.Quack();
        }

        public string Fly()
        {
            return _duck.Fly();
        }
    }
}
