using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace WorkSearch.Models
{
    [Table("company")]
    [Index(nameof(Name), IsUnique = true)]
    public class Company
    {
        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name", TypeName = "VARCHAR(70)")]
        public string Name { get; set; }

        [Column("place_of_residence", TypeName = "VARCHAR(70)")]
        public string PlaceOfResidence { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
