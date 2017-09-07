using System.Linq;
using System.Threading.Tasks;
using LvivCompany.Bookstore.BusinessLogic;
using LvivCompany.Bookstore.BusinessLogic.Mapper;
using LvivCompany.Bookstore.BusinessLogic.Services;
using LvivCompany.Bookstore.BusinessLogic.ViewModels;
using LvivCompany.Bookstore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace LvivCompany.Bookstore.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private RoleManager<Role> _roleManager;
        private IMapper<User, EditProfileViewModel> _profileMapper;
        private IMapper<User, RegisterViewModel> _registerMapper;
        private IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IMapper<User, EditProfileViewModel> profileMapper, IMapper<User, RegisterViewModel> registerMapper, IConfiguration configuration, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _profileMapper = profileMapper;
            _registerMapper = registerMapper;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        [HttpGet]
        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _registerMapper.Map(model);
                user.Photo = new PathsToDefaultImages(_configuration).DefaultProfileImage;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    Role approle = await _roleManager.FindByIdAsync(model.AppRoleId.ToString());
                    if (approle != null)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, approle.Name);
                        if (roleResult.Succeeded)
                        {
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            var callbackUrl = Url.Action(
                       "ConfirmEmail",
                       "Account",
                       new { userId = user.Id, code = code },
                       protocol: HttpContext.Request.Scheme);
                            EmailService emailService = new EmailService(_configuration);
                            await emailService.SendEmailAsync(model.Email, "Confirm your account", model.FirstName + $" ,thank you for registration.\nConfirm registration by clicking on the link: <a href='{callbackUrl}'>Confirm Registration</a>");
                            await _signInManager.SignInAsync(user, false);
                            return RedirectToAction("Index", "Home");
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                var isRealPassword = await _userManager.CheckPasswordAsync(user, model.Password);
                if (user != null && isRealPassword)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError("", "You didn`t confirm your email");
                        return View(model);
                    }
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
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

        [Authorize(Roles = "Seller,Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Seller,Customer")]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            EditProfileViewModel model = new EditProfileViewModel();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            model = _profileMapper.Map(currentUser);
            return View("Profile", model);
        }

        [Authorize(Roles = "Seller,Customer")]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            EditProfileViewModel model = new EditProfileViewModel();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            model = _profileMapper.Map(currentUser);

            return View("Edit", model);
        }

        [Authorize(Roles = "Seller,Customer")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (model.Image != null)
                {
                    user.Photo = await new UploadFile().RetrieveFilePath(model.Image, _configuration);
                }

                user = _profileMapper.Map(model, user);
                await _userManager.UpdateAsync(user);

                return RedirectToAction("Profile", "Account");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    IdentityResult result =
                                           await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Profile", "Account");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User is not found");
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}