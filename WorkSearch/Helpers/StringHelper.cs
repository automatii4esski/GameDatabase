using WorkSearch.Models;
using WorkSearch.Helpers.Messages;

namespace WorkSearch.Helpers
{
    public static class StringHelper
    {

        public static string GetSoleProprietorName(User? user)
        {
            return user != null ? $"ИП {user.Surname} {user.Name} {user.Patronymic}" : string.Empty;
        }

        public static string GetStringIfSpecified(string? str)
        {
             return str == null ? MiscMessages.NotSpecified :  str;
        }
    }
}
