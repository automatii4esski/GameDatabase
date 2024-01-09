using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkSearch.DBContext;
using WorkSearch.Helpers;
using WorkSearch.Helpers.Messages;
using WorkSearch.Models;
using WorkSearch.ViewModels.Account;

namespace WorkSearch.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly MyDBContext _dbContext;

        public AccountController(MyDBContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            try
            {
                viewModel.User.UserName = viewModel.User.Email;
                var result = await _userManager.CreateAsync(viewModel.User, viewModel.Password);
                
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(viewModel.User, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return Register();
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Register));
            }
        }

        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.IsRemember, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", ErrorMessages.InvalidLoginAttempt);

                return Login();
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Register));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }
        
        public IActionResult Profile()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null) throw new Exception(ErrorMessages.UserNotFound);

                var user = _dbContext.Users.Include(u => u.Gender).Include(u => u.MainLanguage).First(u => u.Id == int.Parse(userId));

                var viewModel = new ProfileViewModel { User = user };

                return View(viewModel);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;

                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Edit(string? id)
        {
            try
            {
                var userId = id ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null) throw new Exception(ErrorMessages.UserNotFound);

                var user = _dbContext.Users.First(u => u.Id == int.Parse(userId));
                var genderOptions = _dbContext.Genders.ToDictionary(g => g.Id, g=>g.Name);
                var languageOptions = _dbContext.Languages.ToDictionary(l => l.Id, l=>l.Name);

                var viewModel = new EditViewModel { User = user, GenderOptions = genderOptions, LanguageOptions = languageOptions };

                return View(viewModel);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel viewModel)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null) throw new Exception(ErrorMessages.UserNotFound);

                var user = _dbContext.Users.First(u => u.Id == int.Parse(userId));

                UpdateUserFields(user, viewModel.User);

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded) return RedirectToAction(nameof(Profile));

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(viewModel);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;

                return RedirectToAction(nameof(Edit));
            }
        }

        private User UpdateUserFields(User originUser, User newUserFields) 
        {
            originUser.Name = newUserFields.Name;
            originUser.Surname = newUserFields.Surname;
            originUser.Patronymic = newUserFields.Patronymic;
            originUser.Email = newUserFields.Email;
            originUser.DateOfBirth = newUserFields.DateOfBirth;
            originUser.PhoneNumber = newUserFields.PhoneNumber;
            originUser.PlaceOfResidence = newUserFields.PlaceOfResidence;
            originUser.GenderId = newUserFields.GenderId;
            originUser.MainLanguageId = newUserFields.MainLanguageId;

            return originUser;
        }
    }
}
