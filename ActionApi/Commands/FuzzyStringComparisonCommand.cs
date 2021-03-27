using CSharpFunctionalExtensions;
using FuzzySharp;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ActionApi.Commands
{
    public class FuzzyStringComparisonCommand : IRequest<Result<int>>
    {
        public string TargetString { get; set; }
        public string ControlString { get; set; }
    }

    public class FuzzyStringComparisonCommandHandler : IRequestHandler<FuzzyStringComparisonCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(FuzzyStringComparisonCommand request, CancellationToken cancellationToken)
        {
            var ratio = await Task.FromResult(Fuzz.WeightedRatio(request.TargetString, request.ControlString));

            return Result.Success(ratio);
        }
    }
}