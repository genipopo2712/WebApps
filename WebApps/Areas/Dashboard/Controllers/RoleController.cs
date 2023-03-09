using Microsoft.AspNetCore.Mvc;
using WebApps.Models;

namespace WebApps.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class RoleController : Controller
    {
        IRoleRepository repository;
        public RoleController(IRoleRepository repository)
        {
            this.repository = repository;
        }
        [ServiceFilter(typeof(ContactFilter))]
        public IActionResult Index()
        {
            return View(repository.GetRoles());
        }
        [ServiceFilter(typeof(ContactFilter))]
        public IActionResult Create()
        {
            return View();
        }
        [ServiceFilter(typeof(ContactFilter))]
        [HttpPost]
        public IActionResult Create(Role obj)
        {
            if (ModelState.IsValid)
            {
                int ret = repository.Add(obj);
                if (ret >0)
                {
                    return Redirect("/dashboard/role");

                }
            }
            return View(obj);
        }
    }
}
