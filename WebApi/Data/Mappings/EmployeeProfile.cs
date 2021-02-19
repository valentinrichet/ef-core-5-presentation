using AutoMapper;
using WebApi.Data.Database.Models;
using WebApi.Data.Dtos;

namespace WebApi.Data.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<CreateEmployeeFromCompanyDto, Employee>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Employee, EmployeeFromCompanyDto>();
            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(
                    e => e.CompanyId,
                    opt => opt.Condition(src => src.CompanyId.HasValue)
                )
                .ForAllOtherMembers(
                    opts => opts.Condition((src, dest, srcMember) => srcMember != null)
                );
            CreateMap<UpdateEmployeeFromCompanyDto, Employee>()
                .ForAllMembers(
                    opts => opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}
