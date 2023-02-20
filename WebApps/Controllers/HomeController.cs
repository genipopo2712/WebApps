using Microsoft.AspNetCore.Mvc;

namespace WebApps.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
