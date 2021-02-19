using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Dtos;
using WebApi.Services.Business;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("employees")]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeBusiness employeeBusiness;

        public EmployeeController(IEmployeeBusiness employeeBusiness)
        {
            this.employeeBusiness = employeeBusiness;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken)
        {
            var employees = await employeeBusiness.GetAllEmployeesAsync(cancellationToken);
            return employees;
        }

        [HttpGet("{employeeId:int}")]
        public async Task<EmployeeDto> GetEmployeeAsync([FromRoute] int employeeId, CancellationToken cancellationToken)
        {
            var employee = await employeeBusiness.GetEmployeeAsync(employeeId, cancellationToken);
            return employee;
        }

        [HttpPost]
        public async Task<EmployeeDto> CreateEmployeeAsync([FromBody] CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken)
        {
            var employee = await employeeBusiness.AddEmployeeAsync(createEmployeeDto, cancellationToken);
            return employee;
        }

        [HttpDelete("{employeeId:int}")]
        public async Task RemoveEmployeeAsync([FromRoute] int employeeId, CancellationToken cancellationToken)
        {
            await employeeBusiness.RemoveEmployeeAsync(employeeId, cancellationToken);
        }

        [HttpPut("{employeeId:int}")]
        public async Task RemoveEmployeeAsync([FromRoute] int employeeId, [FromBody] UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken)
        {
            await employeeBusiness.UpdateEmployeeAsync(employeeId, updateEmployeeDto, cancellationToken);
        }
    }
}
