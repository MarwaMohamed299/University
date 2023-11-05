using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniversitySystem.Dtos;
using UniversitySystem.Models;

namespace UniversitySystem.Controllers.User
{
    public class UserController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManagers;

        public UserController(UserManager < IdentityUser>userManager,
            SignInManager<IdentityUser>signInManager)
        {

        }
        [HttpPost]
        public IActionResult Login(LoginDto credentials)
        {
            var user = _userManager.FindByEmailAsync(credentials.UserName).Result;
            if ( user == null)
            {
                ModelState.AddModelError("", "Credentials are not correct");
                return View();
            }

            var isAuthenticated = _userManager.CheckPasswordAsync(user, credentials.Password).Result;
            if (isAuthenticated == false)
            {
                ModelState.AddModelError("", "credentials are not correct");
                return View();
            }
            var claims = _userManager.GetClaimsAsync(user).Result;

            _signInManagers.SignInWithClaimsAsync(user, true, claims).Wait();
            return RedirectToAction("Index", "Home");



        }




        [HttpPost]
        public IActionResult Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Department = registerDto.Department
            };

            var CreationResult = _userManager.CreateAsync(user, registerDto.Password).Result;

            if (!CreationResult.Succeeded)
            {
                foreach ( var item in CreationResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View();
            }

            var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id),
            new ("Role", "Student"),
        };

            var addingClaimsResult = _userManager.AddClaimsAsync(user, claims).Result;
            if (!addingClaimsResult.Succeeded)
            {
                foreach (var item in addingClaimsResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }


            return RedirectToAction("Login");


        }

        [HttpPost]
        public IActionResult Logout()
        {

            _signInManagers.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }
        
    }
}
