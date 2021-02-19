using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Database.Models
{
    public abstract class DatabaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
