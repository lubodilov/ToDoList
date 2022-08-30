using ToDoList.Models;
using ToDoList.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {

            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName = userDTO.Email,
                    Email = userDTO.Email
                };


                IdentityResult result = await this.userManager.CreateAsync(user, userDTO.Password);

                if (result.Succeeded)
                {
                    TempData["signupDefaultMessage"] = null;
                    return RedirectToAction(nameof(Login));
                }

                List<IdentityError> errors = new List<IdentityError>(result.Errors);
                ViewData["errors"] = errors;

            }

            TempData["signupDefaultMessage"] = false;
            return View(userDTO);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {

                Microsoft.AspNetCore.Identity.SignInResult result = await this.signInManager
                    .PasswordSignInAsync(userDTO.Email,
                                         userDTO.Password,
                                         false,
                                         false);

                if (result.Succeeded)
                {

                    TempData["loginDefaultMessage"] = null;
                    return RedirectToAction(nameof(Index), "Home");
                }


            }

            TempData["loginDefaultMessage"] = false;
            return View(userDTO);
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
