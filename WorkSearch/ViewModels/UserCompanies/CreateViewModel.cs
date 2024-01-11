using Microsoft.Extensions.FileProviders;
using WorkSearch.Models;

namespace WorkSearch.ViewModels.UserCompanies
{
    public class CreateViewModel
    {
        public Company Company { get; set; } = new();

        public IFormFile? Photo { get; set; }
    }
}
