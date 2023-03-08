using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApps.Models;

namespace WebApps.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class WorkController : Controller
    {
        IWorkRepository repository;
        IHubContext<ChatHub> hubContext;
        public WorkController(IWorkRepository repository,IHubContext<ChatHub> hubContext)
        {
            this.repository = repository;
            this.hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return Json(repository.GetWorks());
        }
        public async Task<IActionResult> Add(Work obj)
        {
            int ret = repository.AddWork(obj);
            if (ret>0)
            {
                await hubContext.Clients.All.SendAsync("reloadWork");
                return Json(ret);
            }
            return Json(0);
        }
        [HttpPost]
        public IActionResult EditChecked(int id)
        {
            return Json(repository.EditChecked(id));
        }
    }
}
