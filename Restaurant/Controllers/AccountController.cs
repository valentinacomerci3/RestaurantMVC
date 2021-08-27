using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Interfaces;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    public class AccountController : Controller
    {
        private readonly IBusinessLayer bl;

        public AccountController(IBusinessLayer businessLayer)
        {
            this.bl = businessLayer;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new AccountViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel vm)
        {
            if (vm == null)
            {
                return View("Error", new ErrorViewModel());
            }

            var account = bl.GetAccount(vm.Username);
            if (account != null && ModelState.IsValid)
            {
                if (account.Password.Equals(vm.Password))
                {
                    //UTENTE AUTENTICATO
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, vm.Username),
                        new Claim(ClaimTypes.Country, "Italy"),
                        new Claim(ClaimTypes.Role, account.Role.ToString())
                    };

                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                        RedirectUri = vm.ReturnUrl
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                        );
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError(nameof(vm.Password), "Invalid password");
                    return View(vm);
                }
            }

            return View(vm);
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
