using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace WorkSearch.Models
{
    [Table("sole_proprietor")]
    [Index(nameof(UserId), IsUnique = true)]
    public class SoleProprietor
    {
        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
