using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Dtos;

namespace WebApi.Services.Business
{
    public interface ICompanyBusiness
    {
        public Task<CompanyDto> AddCompanyAsync(CreateOrUpdateCompanyDto createOrUpdateCompanyDto, CancellationToken cancellationToken);
        public Task<EmployeeFromCompanyDto> AddEmployeeToCompanyAsync(int companyId, CreateEmployeeFromCompanyDto createEmployeeFromCompanyDto, CancellationToken cancellationToken);
        public Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(CancellationToken cancellationToken);
        public Task<CompanyDto> GetCompanyAsync(int companyId, CancellationToken cancellationToken);
        public Task RemoveCompanyAsync(int companyId, CancellationToken cancellationToken);
        public Task RemoveEmployeeFromCompanyAsync(int companyId, int employeeId, CancellationToken cancellationToken);
        public Task UpdateCompanyAsync(int companyId, CreateOrUpdateCompanyDto createOrUpdateCompanyDto, CancellationToken cancellationToken);
    }
}
