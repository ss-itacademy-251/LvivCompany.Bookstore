using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvivCompany.Bookstore.Web.ViewModels;
using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private RoleManager<IdentityRole<long>> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<long>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            var role = _roleManager.FindByNameAsync("Admin");
            if (role==null)
            {
                _roleManager.CreateAsync(new IdentityRole<long>("Admin"));
                _roleManager.CreateAsync(new IdentityRole<long>("Customer"));
                _roleManager.CreateAsync(new IdentityRole<long>("Seller"));
            }
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
                User user = new User { Email = model.Email, UserName = model.Email, FirstName = model.FirstName, Address1 = model.Address1, Address2 = model.Address2, PhoneNumber = model.PhoneNumber, LastName = model.LastName };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    IdentityRole<long> approle = await _roleManager.FindByIdAsync(model.AppRoleId.ToString());
                    if (approle != null)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, approle.Name);
                        if (roleResult.Succeeded)
                        {
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
            return View(model);
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

                    if (!String.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
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
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            RegisterViewModel model = new RegisterViewModel();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            model.Address1 = currentUser.Address1;
            model.Address2 = currentUser.Address2;
            model.Email = currentUser.Email;
            model.FirstName = currentUser.FirstName;
            model.LastName = currentUser.LastName;
            model.PhoneNumber = currentUser.PhoneNumber;
            
            
          
                return View("Profile", model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(IFormFile formFile)
        {
       
            RegisterViewModel model = new RegisterViewModel();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            model.Address1 = currentUser.Address1;
            model.Address2 = currentUser.Address2;
            model.Email = currentUser.Email;
            model.FirstName = currentUser.FirstName;
            model.LastName = currentUser.LastName;
            model.PhoneNumber = currentUser.PhoneNumber;
          
            return View("Edit",model);
        }
         [HttpPost]
        public async Task<IActionResult> Edit(RegisterViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address1 = model.Address1;
            user.Address2 = model.Address2;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            using (var memoryStream = new MemoryStream())
            {
                model.Photo.CopyTo(memoryStream);
                user.Photo = memoryStream.ToArray();
            }
                await _userManager.UpdateAsync(user);

            return  RedirectToAction("Profile", "Account");
        }

    }
}
