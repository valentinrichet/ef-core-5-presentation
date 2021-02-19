using AutoMapper;
using WebApi.Data.Database.Models;
using WebApi.Data.Dtos;

namespace WebApi.Data.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<CreateOrUpdateRoleDto, Role>();
        }
    }
}
