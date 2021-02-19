using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Dtos
{
    public class UpdateEmployeeDto
    {
        public int? CompanyId { get; set; }
        [MinLength(1)]
        public string Name { get; set; }
        [MinLength(1)]
        public ICollection<int> RoleIds { get; set; }
    }
}
