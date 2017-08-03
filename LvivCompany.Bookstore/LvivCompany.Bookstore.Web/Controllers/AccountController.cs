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
using AutoMapper;

namespace LvivCompany.Bookstore.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private RoleManager<IdentityRole<long>> _roleManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<long>> roleManager,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
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
                User user = _mapper.Map<User>(model); //new User { Email = model.Email, UserName = model.Email, FirstName = model.FirstName, Address1 = model.Address1, Address2 = model.Address2, PhoneNumber = model.PhoneNumber, LastName = model.LastName };
                user.UserName = user.Email;
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
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            EditProfileViewModel model = new EditProfileViewModel();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            model = _mapper.Map<EditProfileViewModel>(currentUser);

            return View("Profile", model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(IFormFile formFile)
        {  EditProfileViewModel model = new EditProfileViewModel();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            model = _mapper.Map<EditProfileViewModel>(currentUser);

            return View("Edit", model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                user = _mapper.Map< EditProfileViewModel,User>(model,user); //*/ModelToUser(model, user);//
                await _userManager.UpdateAsync(user);

                return RedirectToAction("Profile", "Account");

            }
            else
            {
                return View(model);
            }
          
                
        }
        public User ModelToUser(EditProfileViewModel model, User tempUser)
        {
        
            tempUser.FirstName = model.FirstName;
            tempUser.LastName = model.LastName;
            tempUser.Address1 = model.Address1;
            tempUser.Address2 = model.Address2;
            tempUser.PhoneNumber = model.PhoneNumber;
            tempUser.Email = model.Email;
            return tempUser;
        }
        public EditProfileViewModel UserToModel(User user)
        {
            EditProfileViewModel model = new EditProfileViewModel();
            model.Address1 = user.Address1;
            model.Address2 = user.Address2;
            model.Email = user.Email;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.PhoneNumber = user.PhoneNumber;
            return model;
        }
    }
}
