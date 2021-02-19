using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Dtos
{
    public class CreateOrUpdateCompanyDto
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
    }
}
