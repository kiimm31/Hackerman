using System;
using TestApi.Interfaces;

namespace TestApi
{
    public class RandomEvent : IEventArgs
    {
        public int Id { get; set; }
        public DateTimeOffset RequestDateTime { get; set; }
    }
}
