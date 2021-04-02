using Action.Domain;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ActionApi.Commands
{
    public class GetPersonnelRequest : IRequest<Result<Personnel>>
    {
        public int UserId { get; set; }
    }

    public class GetPersonnelRequestHandler : IRequestHandler<GetPersonnelRequest, Result<Personnel>>
    {
        private readonly ActionContext _actionContext;

        public GetPersonnelRequestHandler(ActionContext actionContext)
        {
            _actionContext = actionContext;
        }

        public async Task<Result<Personnel>> Handle(GetPersonnelRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var personnel = await _actionContext.Personnels.SingleAsync(x => x.Id == request.UserId);

                return Result.SuccessIf<Personnel>(personnel != null, personnel, "Unable to find Personnel");
            }
            catch (System.Exception ex)
            {
                return Result.Failure<Personnel>(ex.GetBaseException().Message);
            }
        }
    }
}
