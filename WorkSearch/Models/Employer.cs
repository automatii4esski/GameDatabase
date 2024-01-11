using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkSearch.Models
{
    public abstract class Employer
    {
        [Column("id")]
        [Key]
        public Guid Id { get; set; }

        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("description", TypeName = "TEXT")]
        public string Description { get; set; }

        [Column("photo_url")]
        public string? PhotoUrl { get; set; }
    }
}
