using Polly;
using Polly.Retry;
using System;

namespace TestApi.Extensions
{
    public static class RetryPolicies
    {
        public static AsyncRetryPolicy GenericRetryPolicy(int retries, int baseTime)
        {
            //Retry Policy
            AsyncRetryPolicy retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    retries,
                    i => TimeSpan.FromSeconds(Math.Pow(baseTime, i)),
                    (exception, timeSpan, retryCount, context) => ManageRetryException(exception, timeSpan, retryCount, context));

            return retryPolicy;
        }

        private static void ManageRetryException(Exception exception, TimeSpan timeSpan, object retryCount, object context)
        {
            string msg = $"Retry n°{retryCount} of {exception.GetBaseException().Message}";
            Console.WriteLine(msg);
        }
    }
}