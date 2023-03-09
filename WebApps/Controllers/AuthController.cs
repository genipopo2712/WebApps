using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Client;
using System.Security.Claims;
using System.Text.RegularExpressions;
using WebApps.Models;

namespace WebApps.Controllers
{
    public class AuthController : Controller
    {
        IMemberRepository repository;
        IHubContext<ChatHub> hubContext;
        public AuthController(IMemberRepository repository, IHubContext<ChatHub> hubContext)
        {
            this.repository = repository;
            this.hubContext = hubContext;
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
                    new Claim("Gender", member.Gender.ToString())
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
        public IActionResult SigninGoogle()
        {
            AuthenticationProperties properties = new AuthenticationProperties
            {
                RedirectUri = "/auth/responsegoogle"
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        public async Task<IActionResult> ResponseGoogle()
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            IEnumerable<Claim>? claims = result.Principal?.Claims;
            if (claims != null)
            {
                //save password
                //hash sql vs hash c# compare ?
                //Member obj = new Member { Gender = true, Avatar = "no-image.jpg", Password = "123" };
                Member obj = new Member { Gender = Gender.Male, Avatar = "no-image.jpg", Password = "123" };
                foreach (Claim claim in claims)
                {
                    switch (claim.Type)
                    {
                        case ClaimTypes.NameIdentifier:
                            obj.MemberId = claim.Value;
                            break;
                        case ClaimTypes.Email:
                            obj.Email = claim.Value;
                            obj.Username = claim.Value;
                            break;
                        case ClaimTypes.Name:
                            obj.Fullname = claim.Value;
                            break;
                        case ClaimTypes.Surname:
                            if (claim.Value.ToLower().Contains("thị"))
                            {
                                obj.Gender = Gender.Female;
                            }
                            break;
                    }
                }
                int ret = repository.AddMemberIfNotExists(obj);
                if (ret != 0)
                {
                    await hubContext.Clients.All.SendAsync("addMember",obj);                    
                }
            }
            return Redirect("/");
        }
        public IActionResult SigninFacebook()
        {
            AuthenticationProperties properties = new AuthenticationProperties
            {
                RedirectUri = "/auth/responsefacebook"
            };
            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }
        public async Task<IActionResult> ResponseFacebook()
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Json(result.Principal?.Claims.Select(i => new
            {
                i.Type,
                i.Value
            }));

        }
    }
}
