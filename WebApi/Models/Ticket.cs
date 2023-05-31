using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("tickets")]
    public class Ticket
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("row")]
        public int Row { get; set; }

        [Column("place")]
        public int Place { get; set; }

        [Column("price")]
        public double Price { get; set; }

        [Column("perfomance_id")]
        public Guid PerfomanceId { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("email")]
        public string Email { get; set; } = null!;

        //public Chair Chair { get; set; } = null!;
        public Perfomance Perfomance { get; set; } = null!;

    }
}
