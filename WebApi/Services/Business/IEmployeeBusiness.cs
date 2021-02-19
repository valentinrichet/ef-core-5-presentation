using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Dtos;

namespace WebApi.Services.Business
{
    public interface IEmployeeBusiness
    {
        public Task<EmployeeDto> AddEmployeeAsync(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken);
        public Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken);
        public Task<EmployeeDto> GetEmployeeAsync(int employeeId, CancellationToken cancellationToken);
        public Task RemoveEmployeeAsync(int employeeId, CancellationToken cancellationToken);
        public Task UpdateEmployeeAsync(int employeeId, UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken);
    }
}
