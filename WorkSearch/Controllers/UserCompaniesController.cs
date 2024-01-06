using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkSearch.DBContext;
using WorkSearch.Models;
using System.Security.Claims;
using WorkSearch.ViewModels.UserCompanies;
using WorkSearch.Helpers.Messages;
using Microsoft.Win32;

namespace WorkSearch.Controllers
{
    [Authorize]
    public class UserCompaniesController : Controller
    {
        private readonly MyDBContext _dbContext;
        private readonly UserManager<User> _userManager;

        public UserCompaniesController(MyDBContext dbContext, UserManager<User> userManager)
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

                var userCompanies = _dbContext.Companies.Where(c => c.UserId == int.Parse(userId)).ToList();

                var viewModel = new IndexViewModel { Companies = userCompanies };

                return View(viewModel);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateViewModel viewModel)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null) throw new Exception(ErrorMessages.UserNotFound);

                viewModel.Company.UserId = int.Parse(userId);

                _dbContext.Companies.Add(viewModel.Company);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                
                return RedirectToAction(nameof(Create));
            }
        }

        public IActionResult Edit(int id)
        {
            var company = _dbContext.Companies.First(c => c.Id == id);
            var viewModel = new EditViewModel { Company = company };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel viewModel)
        {
            try
            {
                _dbContext.Companies.Update(viewModel.Company);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;

                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public IActionResult Delete(Company company)
        {
            try
            {
                _dbContext.Companies.Remove(company);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
