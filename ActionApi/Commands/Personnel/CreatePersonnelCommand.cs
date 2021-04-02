using Action.Domain;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ActionApi.Commands
{
    public class CreatePersonnelCommand : IRequest<Result<int>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }

    public class CreatePersonnelCommandHandler : IRequestHandler<CreatePersonnelCommand, Result<int>>
    {
        private readonly ActionContext _context;
        private readonly IMapper _autoMapper;

        public CreatePersonnelCommandHandler(ActionContext context, IMapper autoMapper)
        {
            _context = context;
            _autoMapper = autoMapper;
        }

        public async Task<Result<int>> Handle(CreatePersonnelCommand request, CancellationToken cancellationToken)
        {
            var personnel = _autoMapper.Map<Personnel>(request);

            var added = await _context.Personnels.AddAsync(personnel, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(added?.Entity.Id ?? 0);
        }
    }
}