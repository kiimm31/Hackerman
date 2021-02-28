using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestApi.Commands
{
    public class GetCurrentWeatherCommand : IRequest<WeatherForecast>
    {
        public string City { get; set; }
    }

    public class GetCurrentWeatherCommandHandler : IRequestHandler<GetCurrentWeatherCommand, WeatherForecast>
    {
        public Task<WeatherForecast> Handle(GetCurrentWeatherCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
