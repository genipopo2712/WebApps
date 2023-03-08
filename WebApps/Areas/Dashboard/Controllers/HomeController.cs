using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using WebApps.Models;

namespace WebApps.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize] //thêm chức năng authorize đăng nhập; BẮT BUỘC ĐĂNG NHẬP ĐỂ VÀO ĐƯỢC TRANG NÀY ( TRONG BÀI LÀ TRANG DASHBOARD)
    public class HomeController : Controller //CountController
    {
        IDistributedCache cache;
        IStatisticRepository repository;
        IMessageRepository messageRepository;
        IContactRepository contactRepository;
        public HomeController(IStatisticRepository repository, IDistributedCache cache, IMessageRepository messageRepository, IContactRepository contactRepository)//:base(contactRepository)
        {
            this.cache = cache;
            this.repository = repository;
            this.messageRepository = messageRepository;
            this.contactRepository = contactRepository;
        }
        [HttpPost]
        public IActionResult Monthly(int year)
        {
            return Json(repository.GetTotalPostsByYear(year));
        }
        public IActionResult TotalPostByType()
        {
            return Json(repository.GetTotalPostsByType());
        }
        [HttpPost]
        public IActionResult AddMessage(Message obj)
        {
            obj.MemberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            messageRepository.Add(obj);
            return Redirect("/dashboard");

        }
        [ServiceFilter(typeof(ContactFilter))]
        public IActionResult Index()
        {
            //string? memberId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (memberId != null)
            //{
            //    //ViewBag.contactsNew = contactRepository.CountContactsWithNew(memberId);
            //    //LoadCountNew(memberId);

            //}
            ViewBag.messages = messageRepository.GetMessages();
            string? countQuestions = cache.GetString("CountQuestions");
            if (string.IsNullOrEmpty(countQuestions))
            {
                int count = repository.CountQuestions();
                ViewBag.totalQuestions = repository.CountQuestions();
                cache.SetString("CountQUestions", count.ToString());
            }
            else
            {
                ViewBag.totalQuestions = Convert.ToInt32(countQuestions);
            }

            string? questionOverAnswerRate = cache.GetString("QuestionOverAnswerRate");
            if (string.IsNullOrEmpty(questionOverAnswerRate))
            {
                double count = repository.QuestionOverAnswerRate();
                ViewBag.questionOverAnswerRate = count;
                cache.SetString("QuestionOverAnswerRate", count.ToString());
            }
            else
            {
                ViewBag.questionOverAnswerRate = Convert.ToDouble(questionOverAnswerRate);
            }
            ViewBag.totalUsers=repository.CountUsers();
            ViewBag.totalComments= repository.CountComments();


            return View();
        }
    }
}
