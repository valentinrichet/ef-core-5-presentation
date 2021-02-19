using AutoMapper;
using WebApi.Data.Database.Models;
using WebApi.Data.Dtos;

namespace WebApi.Data.Mappings
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<Company, CompanyFromEmployeeDto>();
            CreateMap<CreateOrUpdateCompanyDto, Company>();
        }
    }
}
