using Action.Domain;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ActionApi.Commands
{
    public class GetAllPersonnelsRequest : IRequest<Result<List<Personnel>>>
    {
        public string NameFilter { get; set; }
    }

    public class GetAllPersonnelsRequestHandler : IRequestHandler<GetAllPersonnelsRequest, Result<List<Personnel>>>
    {
        private readonly ActionContext _context;

        public GetAllPersonnelsRequestHandler(ActionContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Personnel>>> Handle(GetAllPersonnelsRequest request, CancellationToken cancellationToken)
        {
            var personnels = await _context.Personnels.Where(x => (x.FirstName.Contains(request.NameFilter) || x.LastName.Contains(request.NameFilter)) 
                                                                  || string.IsNullOrWhiteSpace(request.NameFilter)).ToListAsync(cancellationToken);

            return Result.Success(personnels);
        }
    }
}
