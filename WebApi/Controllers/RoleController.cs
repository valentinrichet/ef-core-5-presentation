using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Data.Dtos;
using WebApi.Services.Business;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("roles")]
    public class RoleController : Controller
    {

        private readonly IRoleBusiness roleBusiness;

        public RoleController(IRoleBusiness roleBusiness)
        {
            this.roleBusiness = roleBusiness;
        }

        [HttpGet]
        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            var roles = await roleBusiness.GetAllRolesAsync(cancellationToken);
            return roles;
        }

        [HttpGet("{roleId:int}")]
        public async Task<RoleDto> GetRoleAsync([FromRoute] int roleId, CancellationToken cancellationToken)
        {
            var role = await roleBusiness.GetRoleAsync(roleId, cancellationToken);
            return role;
        }

        [HttpPost]
        public async Task<RoleDto> CreateRoleAsync([FromBody] CreateOrUpdateRoleDto createOrUpdateRoleDto, CancellationToken cancellationToken)
        {
            var role = await roleBusiness.AddRoleAsync(createOrUpdateRoleDto, cancellationToken);
            return role;
        }

        [HttpDelete("{roleId:int}")]
        public async Task RemoveRoleAsync([FromRoute] int roleId, CancellationToken cancellationToken)
        {
            await roleBusiness.RemoveRoleAsync(roleId, cancellationToken);
        }

        [HttpPut("{roleId:int}")]
        public async Task RemoveRoleAsync([FromRoute] int roleId, [FromBody] CreateOrUpdateRoleDto createOrUpdateRoleDto, CancellationToken cancellationToken)
        {
            await roleBusiness.UpdateRoleAsync(roleId, createOrUpdateRoleDto, cancellationToken);
        }
    }
}
