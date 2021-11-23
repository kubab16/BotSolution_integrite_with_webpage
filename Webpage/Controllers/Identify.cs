using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Webpage.Controllers
{
    public class Identify : Controller
    {
        private readonly Context _context;

        public Identify(Context context)
        {
            _context = context;
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost("login")]
        public async Task<IActionResult> ActionResult(string mail, string password, bool RememberMe, string returnUrl)
        {
            if (mail == null || password == null)
                return View("Login");
            ViewData["ReturnUrl"] = returnUrl ?? TempData["ReturnUrl"];
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Mail == mail);
            if (user != null)
                if (BCrypt.Net.BCrypt.Verify(password, user.HashedPassord))
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("username", user.Name));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Name));
                    claims.Add(new Claim(ClaimTypes.Name, user.Name));
                    claims.Add(new Claim("id", Convert.ToString( user.Id)));
                    if(!RememberMe)
                        claims.Add(new Claim(ClaimTypes.Expiration, DateTime.Now.AddHours(12).ToString() ));
                    var claimsIdebtity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdebtity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            TempData["mail"] = mail;
            TempData["ErrorLogin"] = "Błedny login lub hasło";
            return View("Login");
        }
        [HttpGet("Register")]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost("Register")]
        public async Task<IActionResult> NewUserAsync(string Nick, string Email, string Password, string ConfirmPassword, DateTime BrightDay, bool Rule, bool Pricacy, string returnUrl)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Mail == Email);
            bool validate = true;
            if (user != null)
            {
                validate = false;
                TempData["mail"] = "Adres email jest już zajęty.";
            }
            if (Password != ConfirmPassword)
            {
                validate = false;
                TempData["EroorPassword"] = "Hasła nie są zgodne!";
            }
            if (Password.Length < 6 || Password.Length > 40)
            {
                validate = false;
                TempData["EroorPassword"] = "Hasło mósi zawierać od 6 do 40 znaków.";
            }
            if (!Rule)
            {
                validate = false;
                TempData["ErrorRule"] = "Musisz zaakceptować reguramin";
            }
            if (!Pricacy)
            {
                validate = false;
                TempData["ErrorPricacy"] = "Musisz zaakceptować reguramin";
            }
            if (!validate)
            {
                TempData["Nick"] = Nick;
                TempData["mail"] = Email;
                TempData["BD"] = BrightDay;
                TempData["ReturnUrl"] = returnUrl;
                return View("Register");
            }
            bool adult = false;
            if (DateTime.Today.AddYears(-18)<= BrightDay)
                adult = true;
            user = new User { Name = Nick, Mail = Email, HashedPassord = BCrypt.Net.BCrypt.HashPassword(Password), BrightDate = BrightDay, Adult = adult };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost("logout")]
        public async Task<IActionResult> logoutAsync(string returnUrl)
        {
            if(User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
