using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkSearch.Models
{
    [Table("gender")]
    public class Gender
    {
        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [Column("name", TypeName = "VARCHAR(15)")]
        public string Name { get; set; }
    }
}
