using CSharpFunctionalExtensions;
using MediatR;
using Polly.Retry;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TestApi.Extensions;

namespace TestApi.Commands
{
    public class GetRandomNumberCommand : IRequest<Result<string>>
    {
    }

    public class GetRandomNumberCommandHandler : IRequestHandler<GetRandomNumberCommand, Result<string>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AsyncRetryPolicy _polly = RetryPolicies.GenericRetryPolicy(3, 3);

        private static string RandomStringApiUrl => "https://www.random.org/strings/?num=1&len=8&digits=on&upperalpha=on&loweralpha=on&unique=on&format=plain&rnd=new";

        public GetRandomNumberCommandHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<string>> Handle(GetRandomNumberCommand request, CancellationToken cancellationToken)
        {
            using var client = _httpClientFactory.CreateClient();
            var policyResult = await _polly.ExecuteAndCaptureAsync(async () => await client.GetAsync(RandomStringApiUrl, cancellationToken));

            if (policyResult.Outcome == Polly.OutcomeType.Successful)
            {
                var httpResponse = policyResult.Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    var randomString = await httpResponse.Content.ReadAsStringAsync();
                    return Result.Success<string>(randomString);
                }
                else
                {
                    return Result.Failure<string>($"Failed to call {RandomStringApiUrl} : {httpResponse.StatusCode} ; {httpResponse.ReasonPhrase}");
                }
            }
            else
                return Result.Failure<string>($"Polly Retry Failed {policyResult.FinalException.GetBaseException().Message}");
        }
    }
}