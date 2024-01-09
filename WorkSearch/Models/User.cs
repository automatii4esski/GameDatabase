using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkSearch.Helpers;
using WorkSearch.Helpers.Messages;
using WorkSearch.Helpers.Validation;

namespace WorkSearch.Models
{
    [Table("user")]
    public class User : IdentityUser<int>
    {
        [Column("id")]
        [PersonalData]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

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

        [Column("date_of_birth", TypeName = "DATE")]
        [MaxDate]
        [MinAgeDate(16)]
        public DateTime DateOfBirth { get; set; }

        [NotMapped]
        public DateOnly DateOfBirthUI { get => DateOnly.FromDateTime(DateOfBirth); }

        [NotMapped]
        public int Age { get => DateHelper.GetAge(DateOfBirth); }

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
