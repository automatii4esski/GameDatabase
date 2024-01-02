using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkSearch.Models
{
    [Table("user")]
    public class User : IdentityUser<long>
    {
        [Column("id")]
        [PersonalData]
        [Key]
        public override long Id { get; set; }

        [Column("name", TypeName = "VARCHAR(70)")]
        public string Name { get; set; }

        [Column("surname", TypeName = "VARCHAR(70)")]
        public string Surname { get; set; }

        [Column("patronymic", TypeName = "VARCHAR(70)")]
        public string Patronymic { get; set; }

        [Column("email", TypeName = "VARCHAR(255)")]
        [ProtectedPersonalData]
        [EmailAddress]
        public override string Email { get; set; }

        [Column("phone", TypeName = "VARCHAR(15)")]
        [ProtectedPersonalData]
        public override string? PhoneNumber { get; set; }

        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [Column("place_of_residence", TypeName = "VARCHAR(70)")]
        public string? PlaceOfResidence { get; set; }

        [Column("gender_id")]
        public byte? GenderId { get; set; }
        public virtual Gender? Gender { get; set; }

        [Column("main_language_id")]
        public short? MainLanguageId { get; set; }
        public virtual Language? MainLanguage { get; set; }
    }
}
