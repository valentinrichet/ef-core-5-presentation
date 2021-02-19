using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Database.Models
{
    public class Company : DatabaseEntity
    {
        public virtual ICollection<Employee> Employee { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
