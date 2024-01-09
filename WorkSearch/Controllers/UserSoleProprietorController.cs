using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkSearch.DBContext;
using WorkSearch.Helpers;
using WorkSearch.Helpers.Messages;
using WorkSearch.Models;
using WorkSearch.ViewModels.UserSoleProprietor;

namespace WorkSearch.Controllers
{
    [Authorize]
    public class UserSoleProprietorController : Controller
    {
        private readonly MyDBContext _dbContext;
        private readonly UserManager<User> _userManager;

        public UserSoleProprietorController(MyDBContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null) throw new Exception(ErrorMessages.UserNotFound);

                var SoleProprietor = _dbContext.SoleProprietors.Include(s => s.User).FirstOrDefault(s => s.UserId == int.Parse(userId));

                var viewModel = new IndexViewModel { SoleProprietor = SoleProprietor, SoleProprietorName = StringHelper.GetSoleProprietorName(SoleProprietor?.User) };

                return View(viewModel);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
