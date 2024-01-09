using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkSearch.Models
{
    public abstract class Employer
    {
        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name", TypeName = "VARCHAR(250)")]
        public string Name { get; set; }

        [Column("description", TypeName = "TEXT")]
        public string Description { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
