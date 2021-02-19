using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Dtos
{
    public class UpdateEmployeeFromCompanyDto
    {
        [MinLength(1)]
        public string Name { get; set; }
        [MinLength(1)]
        public ICollection<int> RoleIds { get; set; }
    }
}
