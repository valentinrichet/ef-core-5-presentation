using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Dtos;
using WebApi.Services.Business;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("companies")]
    public class CompanyController : Controller
    {
        private readonly ICompanyBusiness companyBusiness;

        public CompanyController(ICompanyBusiness companyBusiness)
        {
            this.companyBusiness = companyBusiness;
        }

        [HttpPost("{companyId:int}/employees")]
        public async Task<EmployeeFromCompanyDto> AddEmployeeToCompanyAsync([FromRoute] int companyId, [FromBody] CreateEmployeeFromCompanyDto createEmployeeFromCompanyDto, CancellationToken cancellationToken)
        {
            var employee = await companyBusiness.AddEmployeeToCompanyAsync(companyId, createEmployeeFromCompanyDto, cancellationToken);
            return employee;
        }

        [HttpGet]
        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(CancellationToken cancellationToken)
        {
            var companies = await companyBusiness.GetAllCompaniesAsync(cancellationToken);
            return companies;
        }

        [HttpGet("{companyId:int}")]
        public async Task<CompanyDto> GetCompanyAsync([FromRoute] int companyId, CancellationToken cancellationToken)
        {
            var company = await companyBusiness.GetCompanyAsync(companyId, cancellationToken);
            return company;
        }

        [HttpPost]
        public async Task<CompanyDto> CreateCompanyAsync([FromBody] CreateOrUpdateCompanyDto createOrUpdateCompanyDto, CancellationToken cancellationToken)
        {
            var company = await companyBusiness.AddCompanyAsync(createOrUpdateCompanyDto, cancellationToken);
            return company;
        }

        [HttpDelete("{companyId:int}")]
        public async Task RemoveCompanyAsync([FromRoute] int companyId, CancellationToken cancellationToken)
        {
            await companyBusiness.RemoveCompanyAsync(companyId, cancellationToken);
        }

        [HttpPut("{companyId:int}")]
        public async Task RemoveCompanyAsync([FromRoute] int companyId, [FromBody] CreateOrUpdateCompanyDto createOrUpdateCompanyDto, CancellationToken cancellationToken)
        {
            await companyBusiness.UpdateCompanyAsync(companyId, createOrUpdateCompanyDto, cancellationToken);
        }

        [HttpDelete("{companyId:int}/employees/{employeeId:int}")]
        public async Task RemoveEmployeeFromCompanyAsync([FromRoute] int companyId, [FromRoute] int employeeId, CancellationToken cancellationToken)
        {
            await companyBusiness.RemoveEmployeeFromCompanyAsync(companyId, employeeId, cancellationToken);
        }
    }
}
