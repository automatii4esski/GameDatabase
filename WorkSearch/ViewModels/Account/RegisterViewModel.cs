using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WorkSearch.Models;
using WorkSearch.Helpers.Messages;

namespace WorkSearch.ViewModels.Account
{
    public class RegisterViewModel : AuthorizationViewModel
    {
        public User User { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = ErrorMessages.InvalidConfirmationPassword)]
        public string ConfirmPassword { get; set; }
    }
}
