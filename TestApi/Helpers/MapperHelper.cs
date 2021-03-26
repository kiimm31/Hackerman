using AutoMapper;
using TestApi.Commands;
using TestApi.Models.Dto;

namespace TestApi.Helpers
{
    public class OcrProfile : Profile
    {
        public OcrProfile()
        {
            CreateMap<PerformOcrCommand, OcrRequest>();
        }
    }
}
