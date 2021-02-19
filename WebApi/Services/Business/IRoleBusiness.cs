using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Dtos;

namespace WebApi.Services.Business
{
    public interface IRoleBusiness
    {
        public Task<RoleDto> AddRoleAsync(CreateOrUpdateRoleDto createOrUpdateRoleDto, CancellationToken cancellationToken);
        public Task<IEnumerable<RoleDto>> GetAllRolesAsync(CancellationToken cancellationToken);
        public Task<RoleDto> GetRoleAsync(int roleId, CancellationToken cancellationToken);
        public Task RemoveRoleAsync(int roleId, CancellationToken cancellationToken);
        public Task UpdateRoleAsync(int roleId, CreateOrUpdateRoleDto createOrUpdateRoleDto, CancellationToken cancellationToken);
    }
}
