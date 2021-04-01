using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ActionApi.Helpers;
using ActionApi.Models.Dto;

namespace ActionApi.Commands
{
    public class PerformOcrCommand : IRequest<Result<string>>
    {
        public int ImageXCoordinate { get; set; }
        public int ImageYCoordinate { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public string ProcessName { get; set; }
        public bool InvertColour { get; set; }
    }

    public class PerformOcrCommandHandler : IRequestHandler<PerformOcrCommand, Result<string>>
    {
        private readonly IMapper _mapper;

        public PerformOcrCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(PerformOcrCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<OcrRequest>(request);

            var extractWordFromImage = await Task.FromResult(ImageHelper.ExtractWordFromImage(dto));

            return extractWordFromImage;
        }
    }
}