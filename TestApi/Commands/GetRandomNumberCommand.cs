using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TestApi.Models;
using Polly.Retry;
using TestApi.Extensions;

namespace TestApi.Commands
{
    public class GetRandomNumberCommand : IRequest<Result<RandomNumberDTO>>
    {

    }

    public class GetRandomNumberCommandHandler : IRequestHandler<GetRandomNumberCommand, Result<RandomNumberDTO>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private AsyncRetryPolicy _polly = RetryPolicies.GenericRetryPolicy(3, 3);

        private static string _randomStringApiUrl => "https://www.random.org/strings/?num=1&len=8&digits=on&upperalpha=on&loweralpha=on&unique=on&format=plain&rnd=new";

        public GetRandomNumberCommandHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Result<RandomNumberDTO>> Handle(GetRandomNumberCommand request, CancellationToken cancellationToken)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var policyResult = await _polly.ExecuteAndCaptureAsync(async () => await client.GetAsync(_randomStringApiUrl));

                if (policyResult.Outcome == Polly.OutcomeType.Successful)
                {
                    var httpResponse = policyResult.Result;
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var returnModel = new RandomNumberDTO()
                        {
                            RandomString = await httpResponse.Content.ReadAsStringAsync()
                        };
                        return Result.Success<RandomNumberDTO>(returnModel);
                    }
                    else
                    {
                        return Result.Failure<RandomNumberDTO>($"Failed to call {_randomStringApiUrl} : {httpResponse.StatusCode} ; {httpResponse.ReasonPhrase}");
                    }
                }
                else
                    return Result.Failure<RandomNumberDTO>($"Polly Retry Failed {policyResult.FinalException.GetBaseException().Message}");
            }

        }
    }
}
