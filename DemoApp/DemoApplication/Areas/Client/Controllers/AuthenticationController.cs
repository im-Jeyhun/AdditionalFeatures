using DemoApplication.Areas.Client.ViewModels.Authentication;
using DemoApplication.Attribute;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace DemoApplication.Controllers
{
    [Area("client")]
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;

        public AuthenticationController(DataContext dbContext , IUserService userService )
        {
            _dbContext = dbContext;
            _userService = userService;

        }

        [HttpGet("login", Name = "client-auth-login")]
        [IsAuthenticated]
        public async Task<IActionResult> LoginAsync()
        {

            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost("login", Name = "client-auth-login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
          
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!await _userService.CheckUserAsync(model.Email, model.Password))
            {
                ModelState.AddModelError(String.Empty, "Email or Password is not correct");

                return View(model);
            }


           await _userService.SignInAsync(model.Email, model.Password);


            return RedirectToRoute("client-home-index");
        }

        [HttpGet("logout", Name = "client-auth-logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _userService.SignOutAsync();
            return RedirectToRoute("client-home-index");
        }


        #region Register

        [HttpGet("register", Name = "client-auth-register")]
        [IsAuthenticated]
        public async Task<IActionResult> RegisterAsync()
        {
          

            var model = new RegisterViewModel();

            return View();
        }


        [HttpPost("register" , Name = "client-auth-register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            
            if (_dbContext.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError(String.Empty, "Email already used");

                return View(model);
            }

            await _userService!.CreateAsync(model);

            return RedirectToRoute("client-auth-login");
        }

        #endregion
    }
}
