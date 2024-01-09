using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Identity;

namespace WorkSearch.Helpers.Messages
{
    public class MyIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format(ErrorMessages.DuplicateEmail, email)
            };
        }
    }
}
