using API.Dto;
using AutoMapper;
using Domain.Models;

namespace API.Mappers
{
    public class WerknemerProfile : Profile
    {
        public WerknemerProfile()
        {
            CreateMap<Werknemer, WerknemerDto>();
            CreateMap<WerknemerDto, Werknemer>();
            CreateMap<WithInsertWerknemerDto, Werknemer>();
            CreateMap<InsertWerknemerCommandDto, Werknemer>();
        }
    }
}
