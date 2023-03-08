using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApps.Models;

namespace WebApps.Areas.Dashboard.Controllers
{
    public abstract class CountController : Controller //Noted this abstract
    {
        protected IContactRepository repository;
        public CountController(IContactRepository repository)
        {
            this.repository = repository;
        }
        protected void LoadCountNew()
        {
            string? memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (memberId != null)
            {
                LoadCountNew(memberId);

            }
        }
        //MUST BE delete this index IF NOT error showing cause this have multiple Index view 
        //public IActionResult Index()
        //{
        //    return View();
        //}
        protected void LoadCountNew(string memberId)
        {
            TotalTimeLine totalTimeLine = repository.CountContactsWithNew(memberId);
            if (totalTimeLine == null)
            {
                totalTimeLine = new TotalTimeLine { Total = 0, Ago = 0, TimeName = "sec" };
            }
            ViewBag.contactsNew = totalTimeLine;
        }
    }
}
