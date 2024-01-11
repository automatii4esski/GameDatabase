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

        public static string GetPhotoPath(string? photoName)
        {
            return "~/photo/" + (photoName ?? "no-photo.jpg");
        }

        public static string GetCuttedText(string text = "", int length = 250)
        {
            return text.Length > length ? text.Substring(0, length) + "..." : text;
        }
    }
}
