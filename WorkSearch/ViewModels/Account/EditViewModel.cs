using WorkSearch.Models;

namespace WorkSearch.ViewModels.Account
{
    public class EditViewModel
    {
        public User User { get; set; } = new();
        public Dictionary<short, string> LanguageOptions { get; set; } = new();
        public Dictionary<byte, string> GenderOptions { get; set; } = new();
    }
}
