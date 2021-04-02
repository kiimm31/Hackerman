using Action.Domain;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ActionApi.Commands
{
    public class AddLicenseCommand : IRequest<Result>
    {
        public int PersonnelId { get; set; }
        public string LicenseName { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }
    }

    public class AddLicenseCommandHandler : IRequestHandler<AddLicenseCommand, Result>
    {
        private readonly ActionContext _context;

        public AddLicenseCommandHandler(ActionContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(AddLicenseCommand request, CancellationToken cancellationToken)
        {
            var personnel = await _context.Personnels.SingleAsync(x => x.Id == request.PersonnelId, cancellationToken);

            var license = new License(personnel.Id, request.LicenseName, request.IssueDate, request.ExpiryDate);

            personnel.AddLicense(license);
            
            return Result.SuccessIf(await _context.SaveChangesAsync(cancellationToken) > 0 , 
                $"Failed to save {request.LicenseName} to {personnel.FirstName} {personnel.LastName}'s Profile");
        }
    }
}
