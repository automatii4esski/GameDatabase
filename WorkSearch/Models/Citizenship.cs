using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkSearch.Models
{
    [Table("citizenship")]
    public class Citizenship
    {
        [Column("id")]
        [Key]
        public short Id { get; set; }

        [Column("name", TypeName = "VARCHAR(45)")]
        public string Name { get; set; }
    }
}
