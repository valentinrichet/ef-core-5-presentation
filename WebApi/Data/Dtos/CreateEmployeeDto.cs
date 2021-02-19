using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Dtos
{
    public class CreateEmployeeDto
    {
        [Required]
        public int CompanyId { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        [Required]
        [MinLength(1)]
        public ICollection<int> RoleIds { get; set; }
    }
}
