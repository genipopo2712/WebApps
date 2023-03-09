using Azure.Core.Serialization;
using Microsoft.AspNetCore.Mvc;
using WebApps.Models;

namespace WebApps.Controllers
{
    [Area("dashboard")]
    public class MemberController : Controller
    {
        IMemberRepository repository;
        IRoleRepository roleRepository;
        IMemberInRoleRepository memberInRoleRepository;
        public MemberController(IMemberRepository repository, IRoleRepository roleRepository, IMemberInRoleRepository memberInRoleRepository)
        {
            this.repository = repository;
            this.roleRepository = roleRepository;
            this.memberInRoleRepository = memberInRoleRepository;
        }
        [HttpPost]
        public IActionResult SaveRole(MemberInRole obj)
        {
            return Json(memberInRoleRepository.Save(obj));

        }
        [ServiceFilter(typeof(ContactFilter))]

        public IActionResult Index()
        {
            return View(repository.GetMembers());
        }
        [ServiceFilter(typeof(ContactFilter))]
        public IActionResult Roles(string id)
        {
            //ViewBag.roles = roleRepository.GetRoles();
            ViewBag.roles = roleRepository.GetRolesChecked(id);
            return View(repository.GetMemberById(id));
        }
    }
}
