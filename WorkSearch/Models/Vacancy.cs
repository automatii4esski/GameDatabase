using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkSearch.Helpers;
using WorkSearch.Helpers.Messages;
using WorkSearch.Helpers.Validation;

namespace WorkSearch.Models
{
    [Table("vacancy")]
    public class Vacancy
    {
        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("employer_id")]
        public Guid EmployerId { get; set; }
        public virtual Employer Employer { get; set; }
    }
}
