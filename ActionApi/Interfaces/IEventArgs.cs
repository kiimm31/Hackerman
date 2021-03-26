using System;

namespace TestApi.Interfaces
{
    public interface IEventArgs
    {
        int Id { get; set; }
        DateTimeOffset RequestDateTime { get; set; }
    }
}