using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Dtos
{
    public class CompanyDto
    {
        public ICollection<EmployeeFromCompanyDto> Employee { get; set; }
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
    }
}
