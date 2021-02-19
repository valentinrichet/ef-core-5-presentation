using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Database.Models
{
    public class Role : DatabaseEntity
    {
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
