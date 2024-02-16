using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SprintGroopWebApplication.Data;
using SprintGroopWebApplication.Models;
using SprintGroopWebApplication.ViewModels;

namespace SprintGroopWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly  UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager; 
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Some mistachio with validation");
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if (user != null)
            {
                var passwordcheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordcheck == true)
                {
                    var signing = await _signInManager.PasswordSignInAsync(user, loginVM.Password,false,false);
                    if (signing.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }

                }
                TempData["Error"] = "Wrong credentials password. Try again";
                return View(loginVM);
            }
            TempData["Error"] = "Wrong credentials2 user not found. Try again";
            return View(loginVM);
        }
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if(!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email is already existing";
                return View(registerVM);
            }

            var newUser = new AppUser()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser,registerVM.Password);
            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return RedirectToAction("Index", "Home");

        }
        [HttpPost]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
