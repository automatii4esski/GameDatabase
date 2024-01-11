using WorkSearch.Models;

namespace WorkSearch.ViewModels.UserSoleProprietor
{
    public class CreateViewModel
    {
        public SoleProprietor SoleProprietor { get; set; } = new();
        public IFormFile? Photo { get; set; }
    }
}
