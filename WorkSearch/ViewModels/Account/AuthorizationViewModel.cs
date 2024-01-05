using System.ComponentModel.DataAnnotations;

namespace WorkSearch.ViewModels.Account
{
    public class AuthorizationViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
