using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buyit.BOL.DTO.Order;
using Buyit.BOL.DTO.Users;
using Buyit.DAL.Services.Interfaces;
using Buyit.Models.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Buyit.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOrderRepository _orderRepository;


        public AccountController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IOrderRepository orderRepository
           )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _orderRepository = orderRepository;

        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVm, string returnUrl)
        {
            if (ModelState.IsValid)
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
                        return RedirectToAction("Index", "home");
                    }
                }

                ModelState.AddModelError("", "Invalid Login Attempt!!!");
                return View(loginVm);
            }

            return View(loginVm);
        }

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
        public async Task<IActionResult> Register(RegisterVM register)
        {
            var res = await _roleManager.RoleExistsAsync("NormalUser");
            if (!res == true)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = "NormalUser"
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
                    await _userManager.AddToRoleAsync(user, "NormalUser");

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
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyOrders()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            User appUser = _userManager.FindByIdAsync(userId).Result;

            var orderItems = await _orderRepository.OrdersByUserAsync(appUser);



            return View(orderItems);
        }
    }
}