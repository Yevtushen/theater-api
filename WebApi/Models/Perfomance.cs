using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("perfomances")]
    public class Perfomance
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("name")] public string Name { get; set; } = null!;

        [Column("description")] public string Description { get; set; } = null!;

        [Column("date")] public DateTimeOffset DateAndTime { get; set; }
    }
}
