using AutoMapper;
using ActionApi.Commands;
using ActionApi.Models.Dto;

namespace ActionApi.MapperProfiles
{
    public class OcrProfile : Profile
    {
        public OcrProfile()
        {
            CreateMap<PerformOcrCommand, OcrRequest>();
        }
    }
}