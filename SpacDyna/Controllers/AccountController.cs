using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpacDyna.Models;

namespace SpacDyna.Controllers
{
    public class AccountController(SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager) : Controller
    {
        
     
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AppUser loginUser)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser user = await _userManager.FindByNameAsync(loginUser.UserName);
            if (user == null)
                return NotFound();

            var result = await _signInManager.PasswordSignInAsync(user, loginUser.PasswordHash, true, true);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AppUser registerUser)
        {
            if (!ModelState.IsValid)
                return View(registerUser);
            var result = await _userManager.CreateAsync(registerUser, registerUser.PasswordHash);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(registerUser);
            }
        }
    }
}
