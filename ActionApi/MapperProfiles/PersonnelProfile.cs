using Action.Domain;
using ActionApi.Commands;
using AutoMapper;

namespace ActionApi.MapperProfiles
{
    public class PersonnelProfile : Profile
    {
        public PersonnelProfile()
        {
            CreateMap<CreatePersonnelCommand, Personnel>();
        }
    }
}