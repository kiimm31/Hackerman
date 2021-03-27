using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesignPattern
{
    #region HeadFirst Example

    public class ObserverPattern
    {
        public ObserverPattern()
        {
            WeatherStation station = new WeatherStation();
            DisplayMonitor displayMonitor = new DisplayMonitor(station);
            station.RegisterObserver(displayMonitor);
            station.Notify();
        }
    }

    public interface IObservable
    {
        void RegisterObserver(IObserver observer);

        void UnregisterObserver(IObserver observer);

        void Notify();
    }

    public interface IObserver
    {
        void Update();
    }

    public interface IDisplay
    {
        void Display();
    }

    public class WeatherStation : IObservable
    {
        private ObservableCollection<IObserver> _observers { get; set; }

        private double _temperature { get; set; }

        public WeatherStation()
        {
            _observers = new ObservableCollection<IObserver>();
        }

        public void Notify()
        {
            if (_observers.Any())
            {
                foreach (IObserver obs in _observers)
                {
                    obs.Update();
                }
            }
        }

        public double GetTemperature()
        {
            return _temperature;
        }

        public void RegisterObserver(IObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void UnregisterObserver(IObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }
    }

    public class DisplayMonitor : IObserver, IDisplay
    {
        private readonly WeatherStation _observable;

        private double _currentTemperature { get; set; }

        public DisplayMonitor(WeatherStation observable)
        {
            _observable = observable;
        }

        public void Display()
        {
            Console.WriteLine(_currentTemperature);
        }

        public void Update()
        {
            _currentTemperature = _observable.GetTemperature();
            Display();
        }
    }

    #endregion HeadFirst Example

    #region System.Observer from microsoft

    public struct Location
    {
        private double lat, lon;

        public Location(double latitude, double longitude)
        {
            this.lat = latitude;
            this.lon = longitude;
        }

        public double Latitude
        { get { return this.lat; } }

        public double Longitude
        { get { return this.lon; } }
    }

    public class LocationTracker : IObservable<Location>
    {
        public LocationTracker()
        {
            observers = new List<IObserver<Location>>();
        }

        private List<IObserver<Location>> observers; //ListOfObservers to call notify

        public IDisposable Subscribe(IObserver<Location> observer) //RegisterObserver -> when register, return Concrete UnregisterMethod
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Location>> _observers;
            private IObserver<Location> _observer;

            public Unsubscriber(List<IObserver<Location>> observers, IObserver<Location> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        } // Concrete UnregisterMethod

        public void TrackLocation(Nullable<Location> loc) //Notify
        {
            foreach (var observer in observers)
            {
                if (!loc.HasValue)
                    observer.OnError(new LocationUnknownException());
                else
                    observer.OnNext(loc.Value);
            }
        }

        public void EndTransmission() //dispose everything
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted(); // proper teardown?

            observers.Clear();
        }
    }

    public class LocationUnknownException : Exception
    {
        internal LocationUnknownException()
        { }
    }

    public class LocationReporter : IObserver<Location>
    {
        private IDisposable unsubscriber; // store the unsubscribe method
        private string instName;

        public LocationReporter(string name) // initialise for display. since now the observers are passing the value, dont need the reference to observerable
        {
            this.instName = name;
        }

        public string Name
        { get { return this.instName; } }

        public virtual void Subscribe(IObservable<Location> provider) // want to subscribe to this observable
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void OnCompleted() // teardown
        {
            Console.WriteLine("The Location Tracker has completed transmitting data to {0}.", this.Name);
            this.Unsubscribe();
        }

        public virtual void OnError(Exception e) //error handling
        {
            Console.WriteLine("{0}: The location cannot be determined.", this.Name);
        }

        public virtual void OnNext(Location value) // update value
        {
            Console.WriteLine("{2}: The current location is {0}, {1}", value.Latitude, value.Longitude, this.Name);
        }

        public virtual void Unsubscribe() //unsubscrib
        {
            unsubscriber.Dispose();
        }
    }

    #endregion System.Observer from microsoft
}