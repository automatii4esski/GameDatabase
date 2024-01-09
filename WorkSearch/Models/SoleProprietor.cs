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
    }
}
