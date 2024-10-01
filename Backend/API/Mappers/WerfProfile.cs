using API.Dto;
using AutoMapper;
using Domain.Models;

namespace API.Mappers
{
    public class WerfProfile : Profile
    {
        public WerfProfile()
        {
            CreateMap<Werf, WerfDto>();
            CreateMap<WerfDto, Werf>();
            CreateMap<InsertWerfCommandDto, Werf>();
        }
    }
}
