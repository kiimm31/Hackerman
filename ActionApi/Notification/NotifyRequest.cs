using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TestApi.Notification
{
    public class NotifyRequest : INotification
    {
        public string Subject { get; set; }
        public string Message { get; set; }
    }

    public class Notifier1 : INotificationHandler<NotifyRequest>
    {
        public Task Handle(NotifyRequest notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Debugging from Notifier 1. Message  : {notification.Message} , Subject {notification.Subject}");
            return Task.CompletedTask;
        }
    }

    public class Notifier2 : INotificationHandler<NotifyRequest>
    {
        public Task Handle(NotifyRequest notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Debugging from Notifier 2. Message  : {notification.Message} , Subject {notification.Subject}");
            return Task.CompletedTask;
        }
    }
}