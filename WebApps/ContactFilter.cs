using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using WebApps.Models;

namespace WebApps
{
    public class ContactFilter : IActionFilter
    {
        IContactRepository repository;
        public ContactFilter(IContactRepository repository)
        {
            this.repository = repository;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is Controller con)
            {
                string? memberId = con.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (memberId != null)
                {
                    TotalTimeLine totalTimeLine = repository.CountContactsWithNew(memberId);
                    if (totalTimeLine == null)
                    {
                        totalTimeLine = new TotalTimeLine { Total = 0, Ago = 0, TimeName = "sec" };
                    }
                    con.ViewBag.contactsNew = totalTimeLine;
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
