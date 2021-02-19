using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Configurations;
using WebApi.Data.Database.Models;
using WebApi.Data.Dtos;
using WebApi.Services.Repositories;

namespace WebApi.Services.Business
{
    public class CompanyBusiness : ICompanyBusiness
    {
        private readonly IDatabaseEntityRepository<Company> companyRepository;
        private readonly IDatabaseEntityRepository<Employee> employeeRepository;
        private readonly IBaseRepository<EmployeeRole> employeeRoleRepository;
        private readonly IMapper mapper;
        private readonly IDatabaseEntityRepository<Role> roleRepository;

        public CompanyBusiness(IDatabaseEntityRepository<Company> companyRepository, IDatabaseEntityRepository<Employee> employeeRepository, IBaseRepository<EmployeeRole> employeeRoleRepository, IMapper mapper, IDatabaseEntityRepository<Role> roleRepository)
        {
            this.companyRepository = companyRepository;
            this.employeeRepository = employeeRepository;
            this.employeeRoleRepository = employeeRoleRepository;
            this.mapper = mapper;
            this.roleRepository = roleRepository;
        }

        public async Task<CompanyDto> AddCompanyAsync(CreateOrUpdateCompanyDto createOrUpdateCompanyDto, CancellationToken cancellationToken)
        {
            var company = mapper.Map<Company>(createOrUpdateCompanyDto);
            company = await companyRepository.AddAsync(company, cancellationToken);
            var companyDto = mapper.Map<CompanyDto>(company);
            return companyDto;
        }

        public async Task<EmployeeFromCompanyDto> AddEmployeeToCompanyAsync(int companyId, CreateEmployeeFromCompanyDto createEmployeeFromCompanyDto, CancellationToken cancellationToken)
        {
            var roleDtos = await mapper.ProjectTo<RoleDto>(roleRepository.GetAllQuery().Where(role => createEmployeeFromCompanyDto.RoleIds.Contains(role.Id))).ToListAsync(cancellationToken);
            if (roleDtos.Count != createEmployeeFromCompanyDto.RoleIds.Count)
            {
                throw new ArgumentException(CompanyErrorMessages.RoleDoesNotExist);
            }
            var employee = mapper.Map<Employee>(createEmployeeFromCompanyDto);
            employee.CompanyId = companyId;
            employee = await employeeRepository.AddAsync(employee, cancellationToken);
            var employeeRoles = createEmployeeFromCompanyDto.RoleIds.Select(roleId =>
            {
                return new EmployeeRole
                {
                    EmployeeId = employee.Id,
                    RoleId = roleId,
                };
            });
            await employeeRoleRepository.AddRangeAsync(employeeRoles, cancellationToken);
            var employeeDto = mapper.Map<EmployeeFromCompanyDto>(employee);
            employeeDto.Roles = roleDtos;
            return employeeDto;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(CancellationToken cancellationToken)
        {
            var companies = await mapper.ProjectTo<CompanyDto>(companyRepository.GetAllQuery()).ToListAsync(cancellationToken);
            return companies;
        }

        public async Task<CompanyDto> GetCompanyAsync(int companyId, CancellationToken cancellationToken)
        {
            var company = await mapper.ProjectTo<CompanyDto>(companyRepository.GetAllQuery()).FirstOrDefaultAsync(company => company.Id == companyId, cancellationToken);
            return company;
        }

        public async Task RemoveCompanyAsync(int companyId, CancellationToken cancellationToken)
        {
            await companyRepository.RemoveAsync(companyId, cancellationToken);
        }

        public async Task RemoveEmployeeFromCompanyAsync(int companyId, int employeeId, CancellationToken cancellationToken)
        {
            var isEmployeeFromGivenCompany = await employeeRepository.GetAllQuery()
                .AnyAsync(employee => employee.Id == employeeId && employee.CompanyId == companyId, cancellationToken);
            if (!isEmployeeFromGivenCompany)
            {
                throw new ArgumentException(CompanyErrorMessages.EmployeeNotFromCompanyError);
            }
            await employeeRepository.RemoveAsync(employeeId, cancellationToken);
        }

        public async Task UpdateCompanyAsync(int companyId, CreateOrUpdateCompanyDto createOrUpdateCompanyDto, CancellationToken cancellationToken)
        {
            var company = mapper.Map<Company>(createOrUpdateCompanyDto);
            company.Id = companyId;
            await companyRepository.UpdateAsync(company, cancellationToken);
        }
    }
}
