using GroupCollabApp.Areas.Identity.Data;
using GroupCollabApp.Data;
using GroupCollabApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GroupCollabApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly GroupCollabAppAuthDbContext _context;

        public HomeController(ILogger<HomeController> logger,  UserManager<AppUser> userManager, GroupCollabAppAuthDbContext context)
        {
            _logger = logger;
            this._userManager = userManager;
            this._context = context;
        }

        public IActionResult Index()
        {
            ViewData["UserID"] = _userManager.GetUserId(this.User); //details of user stored during login
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Chat");
            }


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Chat()
        {
            return View();
        }

        public IActionResult Calendar()
        {

            return View();
        }
        

        public IActionResult TaskTracker()
        {
            var tasks = _context.TaskItems.ToList();
            return View(tasks);
        }
    }
}