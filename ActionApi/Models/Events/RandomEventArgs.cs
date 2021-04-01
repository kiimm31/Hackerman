using System;
using ActionApi.Interfaces;

namespace ActionApi.Models.Events
{
    public class RandomEventArgs : IEventArgs
    {
        public int Id { get; set; }
        public DateTimeOffset RequestDateTime { get; set; }
    }
}