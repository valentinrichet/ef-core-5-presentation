using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Database.Models;
using WebApi.Data.Dtos;
using WebApi.Services.Repositories;

namespace WebApi.Services.Business
{
    public class RoleBusiness : IRoleBusiness
    {
        private readonly IDatabaseEntityRepository<Role> roleRepository;
        private readonly IMapper mapper;


        public RoleBusiness(IDatabaseEntityRepository<Role> roleRepository, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.mapper = mapper;
        }

        public async Task<RoleDto> AddRoleAsync(CreateOrUpdateRoleDto createOrUpdateRoleDto, CancellationToken cancellationToken)
        {
            var role = mapper.Map<Role>(createOrUpdateRoleDto);
            role = await roleRepository.AddAsync(role, cancellationToken);
            RoleDto roleDto = mapper.Map<RoleDto>(role);
            return roleDto;
        }

        public async Task<RoleDto> GetRoleAsync(int roleId, CancellationToken cancellationToken)
        {
            var role = await mapper.ProjectTo<RoleDto>(roleRepository.GetAllQuery()).FirstOrDefaultAsync(role => role.Id == roleId, cancellationToken);
            return role;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            var roles = await mapper.ProjectTo<RoleDto>(roleRepository.GetAllQuery()).ToListAsync(cancellationToken);
            return roles;
        }

        public async Task RemoveRoleAsync(int roleId, CancellationToken cancellationToken)
        {
            await roleRepository.RemoveAsync(roleId, cancellationToken);
        }

        public async Task UpdateRoleAsync(int roleId, CreateOrUpdateRoleDto createOrUpdateRoleDto, CancellationToken cancellationToken)
        {
            var role = mapper.Map<Role>(createOrUpdateRoleDto);
            role.Id = roleId;
            await roleRepository.UpdateAsync(role, cancellationToken);
        }
    }
}
