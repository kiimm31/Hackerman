using Action.Domain;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ActionApi.Commands
{
    public class DeleteLicenseCommand : IRequest<Result>
    {
        public int PersonnelId { get; set; }
        public int LicenseId { get; set; }
    }

    public class DeleteLicenseCommandHandler : IRequestHandler<DeleteLicenseCommand, Result>
    {
        private readonly ActionContext _context;

        public DeleteLicenseCommandHandler(ActionContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteLicenseCommand request, CancellationToken cancellationToken)
        {
            var personnel = await _context.Personnels.SingleAsync(x => x.Id == request.PersonnelId);

            var license = personnel.Licenses.RemoveAll(x => x.Id == request.LicenseId);

            //license.Delete();

            return Result.SuccessIf(await _context.SaveChangesAsync(cancellationToken) > 0, $"Failed to delete {request.LicenseId} from {personnel.FirstName} {personnel.LastName}'s Profile");
        }
    }
}
