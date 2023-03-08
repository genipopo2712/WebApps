using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.Contracts;
using WebApps.Models;

namespace WebApps.Controllers
{
    public class HomeController : Controller
    {
        IContactRepository repository;
        IHubContext<ChatHub> hubContext;
        public HomeController(IContactRepository repository, IHubContext<ChatHub> hubContext)
        {
            this.repository = repository;
            this.hubContext = hubContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactAsync(Contact obj)
        {
            int ret = repository.Add(obj);
            if (ret > 0)
            {
                await hubContext.Clients.All.SendAsync("countContacts");
                return Redirect("/home/success");
            }
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
