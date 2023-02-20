using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApps.Models;

namespace WebApps.Controllers
{
    public class AuthController : Controller
    {
        IMemberRepository repository;
        public AuthController(IMemberRepository repository)
        {
            this.repository = repository;
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/auth/login");
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult Login(LoginModel obj)
        {
            Member member= repository.Login(obj);
            if (member != null) 
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, member.Username),
                    new Claim(ClaimTypes.Email, member.Email),
                    new Claim(ClaimTypes.NameIdentifier, member.MemberId),
                    new Claim(ClaimTypes.GivenName, member.Fullname),
                    new Claim("Gender", member.Gender ? "Male" : "Female")
                };
                ClaimsIdentity identity= new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties
                {
                    IsPersistent = obj.Rem
                });
            return Redirect("/");
            }
            ModelState.AddModelError("", "Login Failed");
            return View(obj);
        }
    }
}
