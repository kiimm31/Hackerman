using Action.Domain;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ActionApi.Commands
{
    public class EditPersonnelCommand : IRequest<Result>
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
    public class EditPersonnelCommandHandler : IRequestHandler<EditPersonnelCommand, Result>
    {
        private readonly ActionContext _actionContext;

        public EditPersonnelCommandHandler(ActionContext actionContext)
        {
            _actionContext = actionContext;
        }

        public async Task<Result> Handle(EditPersonnelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var personnel = await _actionContext.Personnels.SingleAsync(x => x.UserId == request.UserId);

                personnel.EditAddress(request.Address);
                personnel.EditEmail(request.Email);

                return Result.SuccessIf(await _actionContext.SaveChangesAsync() > 0, "Failed to save to DB");
            }
            catch (System.Exception ex)
            {
                return Result.Failure(ex.GetBaseException().Message);
            }

        }
    }
}