using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkSearch.DBContext;
using WorkSearch.Models;
using System.Security.Claims;
using WorkSearch.ViewModels.UserCompanies;
using WorkSearch.Helpers.Messages;
using Microsoft.Win32;
using Microsoft.EntityFrameworkCore;
using WorkSearch.Helpers;

namespace WorkSearch.Controllers
{
    [Authorize]
    public class UserCompaniesController : MyBaseController
    {
        private readonly MyDBContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UserCompaniesController(MyDBContext dbContext, UserManager<User> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
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

                string? photoName = FileHelper.SavePhoto(_hostingEnvironment, viewModel.Photo);

                viewModel.Company.PhotoUrl = photoName;
                viewModel.Company.UserId = int.Parse(userId);
                viewModel.Company.Id = Guid.NewGuid();

                _dbContext.Companies.Add(viewModel.Company);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                
                return RedirectToAction(Request.Headers["Referer"]);
            }
        }

        public IActionResult Edit(Guid id)
        {
            try
            {
                var company = _dbContext.Companies.First(c => c.Id == id);
                var viewModel = new EditViewModel { Company = company };

                return View(viewModel);
            }
             catch (Exception e)
            {
                TempData["error"] = e.Message;

                return TryRedirectBack();
            }
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel viewModel)
        {
            try
            {
                _dbContext.Companies.Update(viewModel.Company);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Details), new { id = viewModel.Company.Id});
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;

                return TryRedirectBack();
            }

        }

        [HttpPost]
        public IActionResult Delete(Company company)
        {
            try
            {
                if(company.PhotoUrl != null) FileHelper.DeletePhoto(_hostingEnvironment, company.PhotoUrl);

                _dbContext.Companies.Remove(company);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;

                return TryRedirectBack();
            }
        }

        
        public IActionResult ChangePhoto(string id)
        {
            var viewModel = new ChangePhotoViewModel { Id = id};
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ChangePhoto(ChangePhotoViewModel viewModel)
        {
            try
            {
                var photoName = FileHelper.SavePhoto(_hostingEnvironment, viewModel.Photo);

                if(photoName == null) return RedirectToAction(nameof(Index));

                var company = _dbContext.Companies.First(c => c.Id.ToString() == viewModel.Id);

                FileHelper.DeletePhoto(_hostingEnvironment, company.PhotoUrl);

                company.PhotoUrl = photoName;

                _dbContext.Companies.Update(company);

                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Details), new { id = viewModel.Id });
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;

                return TryRedirectBack();
            }
        }

        [HttpPost]
        public IActionResult DeletePhoto(string photoName, string id)
        {
            try
            {
                if (photoName != "no-photo.jpg")
                {
                    var company = _dbContext.Companies.First(c => c.Id.ToString() == id);

                    FileHelper.DeletePhoto(_hostingEnvironment, photoName);

                    company.PhotoUrl = null;

                    _dbContext.Companies.Update(company);
                    _dbContext.SaveChanges();
                }

                return TryRedirectBack();
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;

                return TryRedirectBack();
            }
        }

        public IActionResult Details(string id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null) throw new Exception(ErrorMessages.UserNotFound);

                var company = _dbContext.Companies.First(c => c.UserId == int.Parse(userId) && c.Id.ToString() == id);

                if(company == null) throw new Exception(ErrorMessages.CompanyNotFound);

                var viewModel = new DetailsViewModel { Company = company };

                return View(viewModel);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;

                return TryRedirectBack();
            }
        }
    }
}
