using inzynierka.Data.Roles;
using inzynierka.Data.ViewModels;
using inzynierka.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pracainz.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzynierka.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<WebUser> _userManager;
        private readonly SignInManager<WebUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<WebUser> userManager, SignInManager<WebUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        public IActionResult Login()
        {
            var respone = new LoginVM();

            return View(respone);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Something gone wrong, try again";
                return View(loginVM);
            }

            TempData["Error"] = "Something gone wrong, try again";
            return View(loginVM);
        }

        public IActionResult Register()
        {
            var respone = new RegisterVM();

            return View(respone);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new WebUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            else
            {
                TempData["Error"] = "Something gone wrong, please try again";
                return View(registerVM);
            }

            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
