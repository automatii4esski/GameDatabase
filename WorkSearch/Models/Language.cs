using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkSearch.Models
{
    [Table("language")]
    public class Language
    {
        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("name", TypeName = "VARCHAR(45)")]
        public string Name { get; set; }
    }
}
