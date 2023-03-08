using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApps.Models;

namespace WebApps.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class ContactController :  Controller //CountController // replace inherit from Contoller to CountController
    {
        IContactRepository repository;
        public ContactController(IContactRepository repository) //: base(repository)
        {
            this.repository = repository;
        }
        [ServiceFilter(typeof(ContactFilter))]
        public IActionResult Index()
        {
            string? memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (memberId !=null)
            {
                //naive
                //ViewBag.contactsNew = repository.CountContactsWithNew(memberId);
                //LoadCountNew(memberId);
                return View(repository.GetContactWithNew(memberId));
            }
            return Redirect("/auth/login");

        }
        public IActionResult CountContactsNew()
        {
            string? memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (memberId != null)
            {
                return Json(repository.CountContactsWithNew(memberId));
            }
            return Json(null);

        }
        [ServiceFilter(typeof(ContactFilter))]
        public IActionResult Details(int id)
        {
            string? memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (memberId != null)
            {
                //LoadCountNew(memberId);
                //ViewBag.contactsNew = repository.CountContactsWithNew(memberId);
                return View(repository.GetContactById(memberId, id));
            }
            return Redirect("/auth/login");
        }

    }
}
