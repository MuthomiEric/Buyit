using System.Threading.Tasks;
using Buyit.BOL.DTO.Users;
using BuyIt.Admin.Models.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BuyIt.Admin.Controllers
{

    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM loginVm, string returnUrl)
        {


            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginVm.Username);
                if (user != null)
                {
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        var result = await _signInManager.PasswordSignInAsync(loginVm.Username, loginVm.Password, loginVm.Rememberme, false);
                        if (result.Succeeded)
                        {
                            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            {
                                Redirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Products");
                            }
                        }

                        ModelState.AddModelError("", "Invalid Login Attempt!!!");
                        return View(loginVm);
                    }

                    ModelState.AddModelError("", $"The User {user.Email} Does Not Exist!!!");
                    return View(loginVm);
                }

                else
                {
                    ModelState.AddModelError("", $"The User {loginVm.Username} Does Not Exist!!!");
                    return View(loginVm);
                }


                //ModelState.AddModelError("", $"The User {user.Email} Does Not Exist!!!");
                //return View(loginVm);
            }

            return View(loginVm);
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsUserAvailable(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} Is Already Taken");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            var res = await _roleManager.RoleExistsAsync("Admin");
            if (!res == true)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = "Admin"
                };

                await _roleManager.CreateAsync(identityRole);
            }




            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    PhoneNumber = register.Phone,
                    UserName = register.Email,

                };



                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");

                    await _signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(register);
        }
        [Authorize]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}