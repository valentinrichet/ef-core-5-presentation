using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Dtos
{
    public class RoleDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
