using System.Collections.Generic;
namespace DesignPattern
{
    public class Probe : IUnits
    {
        public Queue<ICommand> Commands { get; set; } = new Queue<ICommand>();

        public int Minerals { get; set; }

        public Point Position { get; set; } = new Point();

        public void Move(int x, int y)
        {
            Commands.Enqueue(new MoveCommand(this, x, y));
        }

        public void Gather()
        {
            Commands.Enqueue(new GatherCommand(this));
        }
    }

    public class MoveCommand : ICommand
    {
        private readonly IUnits _unit;
        private readonly int _x;
        private readonly int _y;

        public MoveCommand(IUnits unit, int x, int y)
        {
            _unit = unit;
            _x = x;
            _y = y;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            _unit.Position.X += _x;
            _unit.Position.Y += _y;
        }
    }

    public class GatherCommand : ICommand
    {
        private readonly IUnits _unit;

        public GatherCommand(IUnits unit)
        {
            _unit = unit;
        }

        public bool CanExecute()
        {
            return _unit.Minerals == 0;
        }

        public void Execute()
        {
            if (CanExecute())
            {
                _unit.Minerals += 5;
            }
        }
    }

    public interface ICommand
    {
        void Execute();
        bool CanExecute();
    }

    public interface IUnits
    {
        int Minerals { get; set; }
        Point Position { get; set; }

        void Move(int x, int y);
        void Gather();
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
