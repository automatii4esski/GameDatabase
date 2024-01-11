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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WorkSearch.Controllers
{
    [Authorize]
    public class UserSoleProprietorController : MyBaseController
    {
        private readonly MyDBContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UserSoleProprietorController(MyDBContext dbContext, UserManager<User> userManager, IWebHostEnvironment hostingEnvironment)
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

                var SoleProprietor = _dbContext.SoleProprietors.Include(s => s.User).FirstOrDefault(s => s.UserId == int.Parse(userId));

                var viewModel = new IndexViewModel { SoleProprietor = SoleProprietor};

                return View(viewModel);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Edit(Guid id)
        {
            try
            {
                var soleProprietor = _dbContext.SoleProprietors.First(s => s.Id == id);
                var viewModel = new EditViewModel { SoleProprietor = soleProprietor };

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
                _dbContext.SoleProprietors.Update(viewModel.SoleProprietor);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;

                return TryRedirectBack();
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

                var user = _dbContext.Users.First(u => u.Id == int.Parse(userId));

                var photoName = FileHelper.SavePhoto(_hostingEnvironment, viewModel.Photo);

                viewModel.SoleProprietor.PhotoUrl = photoName;
                viewModel.SoleProprietor.Name = StringHelper.GetSoleProprietorName(user);
                viewModel.SoleProprietor.UserId = int.Parse(userId);

                _dbContext.SoleProprietors.Add(viewModel.SoleProprietor);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult ChangePhoto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePhoto(ChangePhotoViewModel viewModel)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null) throw new Exception(ErrorMessages.UserNotFound);

                var photoName = FileHelper.SavePhoto(_hostingEnvironment, viewModel.Photo);
                if (photoName == null) return TryRedirectBack();

                var soleProprietor = _dbContext.SoleProprietors.First(c => c.UserId == int.Parse(userId));

                FileHelper.DeletePhoto(_hostingEnvironment, soleProprietor.PhotoUrl);

                soleProprietor.PhotoUrl = photoName;

                _dbContext.SoleProprietors.Update(soleProprietor);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
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
                    var soleProprietor = _dbContext.SoleProprietors.First(s => s.Id.ToString() == id);

                    FileHelper.DeletePhoto(_hostingEnvironment, photoName);

                    soleProprietor.PhotoUrl = null;

                    _dbContext.SoleProprietors.Update(soleProprietor);
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

        [HttpPost]
        public IActionResult Delete(SoleProprietor soleProprietor)
        {
            try
            {
                if (soleProprietor.PhotoUrl != null) FileHelper.DeletePhoto(_hostingEnvironment, soleProprietor.PhotoUrl);

                _dbContext.SoleProprietors.Remove(soleProprietor);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return TryRedirectBack();
            }
        }
    }
}
