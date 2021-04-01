using System;

namespace ActionApi.Interfaces
{
    public interface IEventArgs
    {
        int Id { get; set; }
        DateTimeOffset RequestDateTime { get; set; }
    }
}