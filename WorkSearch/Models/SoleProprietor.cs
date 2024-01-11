using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace WorkSearch.Models
{
    [Table("sole_proprietor")]
    [Index(nameof(UserId), IsUnique = true)]
    public class SoleProprietor : Employer
    {
        [Column("user_id")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
