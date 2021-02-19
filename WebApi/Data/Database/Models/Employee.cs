using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Database.Models
{
    public class Employee : DatabaseEntity
    {
        [Required]
        public virtual Company Company { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual ICollection<Role> Roles { get; set; }
    }
}
