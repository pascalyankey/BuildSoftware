using API.Dto;
using AutoMapper;
using Domain.Models;

namespace API.Mappers
{
    public class RolProfile : Profile
    {
        public RolProfile()
        {
            CreateMap<InsertRolCommandDto, Rol>().ReverseMap();
        }
    }
}
