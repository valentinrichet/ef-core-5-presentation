using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Database.Models
{
    public class EmployeeRole
    {
        [Required]
        public virtual Employee Employee { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public virtual Role Role { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
