using Action.Domain;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ActionApi.Commands
{
    public class DeletePersonnelCommand : IRequest<Result>
    {
        public int UserId { get; set; }
    }
    public class DeletePersonnelCommandHandler : IRequestHandler<DeletePersonnelCommand, Result>
    {
        private readonly ActionContext _context;

        public DeletePersonnelCommandHandler(ActionContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeletePersonnelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var personnel = await _context.Personnels.SingleAsync(x => x.UserId == request.UserId, cancellationToken);

                _context.Personnels.Remove(personnel);

                return Result.SuccessIf(await _context.SaveChangesAsync(cancellationToken) > 0, $"Failed to Delete user: {request.UserId}");
            }
            catch (System.Exception ex)
            {
                return Result.Failure(ex.GetBaseException().Message);
            }
        }
    }
}

