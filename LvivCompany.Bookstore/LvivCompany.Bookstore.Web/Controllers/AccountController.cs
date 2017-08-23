using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.Web.ViewModels;
using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using LvivCompany.Bookstore.Web.Mapper;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private RoleManager<IdentityRole<long>> _roleManager;
        private IMapper<User, EditProfileViewModel> _profileMapper;
        private IMapper<User, RegisterViewModel> _registerMapper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<long>> roleManager, IMapper<User, EditProfileViewModel> profileMapper, IMapper<User, RegisterViewModel> registerMapper, ILogger<AccountController> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _profileMapper = profileMapper;
            _registerMapper = registerMapper;
        }


        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();

            model.AppRoles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()

            }).ToList();
            var itemToRemove = model.AppRoles.Single(r => r.Text == "Admin");
            model.AppRoles.Remove(itemToRemove);

            return View("Register", model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                User user = _registerMapper.Map(model);
             
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    IdentityRole<long> approle = await _roleManager.FindByIdAsync(model.AppRoleId.ToString());
                    if (approle != null)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, approle.Name);
                        if (roleResult.Succeeded)
                        {
                            _logger.LogInformation("Registered new user {@User}", new { FirstName = user.FirstName, LastName = user.LastName,UserName = user.UserName, AppRole=approle.Name, PhoneNumber=user.PhoneNumber });
                            await _signInManager.SignInAsync(user, false);
                            return RedirectToAction("Login", "Account");
                        }
                    }

                }
                else
                {

                    foreach (var error in result.Errors)
                    {

                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }
            }
            model.AppRoles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()

            }).ToList();
            var itemToRemove = model.AppRoles.Single(r => r.Text == "Admin");
            model.AppRoles.Remove(itemToRemove);
            return View("Register", model);
        }
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {                   
                    _logger.LogInformation("Log in User {@User}", new { Email = model.Email });
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }                   
                }
                else
                {
                    ModelState.AddModelError("", "Email  or password is incorrect");
                }                
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {

            await _signInManager.SignOutAsync();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            _logger.LogInformation("User {@User} logged off", new { UserName = currentUser.UserName});
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            EditProfileViewModel model = new EditProfileViewModel();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            model = _profileMapper.Map(currentUser);

            return View("Profile", model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            EditProfileViewModel model = new EditProfileViewModel();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            model = _profileMapper.Map(currentUser);

            return View("Edit", model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                user = _profileMapper.Map(model, user);
                await _userManager.UpdateAsync(user);
                _logger.LogInformation("Edited user {@User}", new { FirstName = user.FirstName, LastName=user.LastName, Email = user.Email });
                return RedirectToAction("Profile", "Account");

            }
            else
            {
                return View(model);
            }


        }

    }
}
