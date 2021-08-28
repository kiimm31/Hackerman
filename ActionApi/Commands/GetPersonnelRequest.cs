using Action.Domain;
using ActionApi.Extensions;
using ActionApi.Factory;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
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
        private readonly IDistributedCache _memoryCache;

        public GetPersonnelRequestHandler(ActionContext actionContext, IDistributedCache memoryCache)
        {
            _actionContext = actionContext;
            _memoryCache = memoryCache;
        }

        public async Task<Result<Personnel>> Handle(GetPersonnelRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var key = CacheKeyFactory.GeneratePersonnelKey(request.UserId);
                var personnelCache = await _memoryCache.GetAsync(key, cancellationToken);

                Personnel personnel = null;
                if (personnelCache != null)
                {
                    personnel = Encoding.UTF8.GetString(personnelCache).Deserialize<Personnel>();
                }
                else
                {
                    personnel = await _actionContext.Personnels.SingleAsync(x => x.Id == request.UserId);

                    await _memoryCache.SetStringAsync(key, personnel.AsJson(), CacheOptionFactory.GenerateDistributedCacheEntryOptions(), cancellationToken);
                }

                return Result.SuccessIf(personnel != null, personnel, "Unable to find Personnel");
            }
            catch (Exception ex)
            {
                return Result.Failure<Personnel>(ex.GetBaseException().Message);
            }
        }
    }
}

