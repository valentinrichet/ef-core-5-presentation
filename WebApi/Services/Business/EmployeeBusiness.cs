using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Database.Models;
using WebApi.Data.Dtos;
using WebApi.Services.Repositories;

namespace WebApi.Services.Business
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private readonly IDatabaseEntityRepository<Employee> employeeRepository;
        private readonly IMapper mapper;


        public EmployeeBusiness(IDatabaseEntityRepository<Employee> employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        public async Task<EmployeeDto> AddEmployeeAsync(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken)
        {
            var employee = mapper.Map<Employee>(createEmployeeDto);
            employee = await employeeRepository.AddAsync(employee, cancellationToken);
            EmployeeDto employeeDto = mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken)
        {
            var employees = await mapper.ProjectTo<EmployeeDto>(employeeRepository.GetAllQuery()).ToListAsync(cancellationToken);
            return employees;
        }

        public async Task<EmployeeDto> GetEmployeeAsync(int employeeId, CancellationToken cancellationToken)
        {
            var employee = await mapper.ProjectTo<EmployeeDto>(employeeRepository.GetAllQuery()).FirstOrDefaultAsync(employee => employee.Id == employeeId, cancellationToken);
            return employee;
        }

        public async Task RemoveEmployeeAsync(int employeeId, CancellationToken cancellationToken)
        {
            await employeeRepository.RemoveAsync(employeeId, cancellationToken);
        }

        public async Task UpdateEmployeeAsync(int employeeId, UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken)
        {
            // With Tracking
            /*
            var employee = await employeeRepository.GetAllQueryWithTracking()
                .Include(employee => employee.EmployeeRoles)
                .SingleAsync(employee => employee.Id == employeeId, cancellationToken);
            mapper.Map(updateEmployeeDto, employee);
            if (updateEmployeeDto.RoleIds != null)
            {
                employee.EmployeeRoles = updateEmployeeDto.RoleIds.Select(roleId => new EmployeeRole
                {
                    EmployeeId = employee.Id,
                    RoleId = roleId,
                }).ToList();
            }
            await employeeRepository.UpdateAsync(employee, cancellationToken);
            */
            var employee = await employeeRepository.GetAllQuery()
                .Include(employee => employee.EmployeeRoles)
                .SingleAsync(employee => employee.Id == employeeId, cancellationToken);
            mapper.Map(updateEmployeeDto, employee);
            if (updateEmployeeDto.RoleIds != null)
            {

                // To delete

                employee.EmployeeRoles = updateEmployeeDto.RoleIds.Select(roleId => new EmployeeRole
                {
                    EmployeeId = employee.Id,
                    RoleId = roleId,
                }).ToList();
            }
            await employeeRepository.UpdateAsync(employee, cancellationToken);
        }
    }
}
