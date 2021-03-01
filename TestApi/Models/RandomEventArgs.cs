using System;
using TestApi.Interfaces;

namespace TestApi.Models
{
    public class RandomEventArgs : IEventArgs
    {
        public int Id { get; set; }
        public DateTimeOffset RequestDateTime { get; set; }
    }
}
