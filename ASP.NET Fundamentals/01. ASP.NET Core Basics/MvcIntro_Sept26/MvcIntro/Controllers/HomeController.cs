using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcIntro.Models;

namespace MvcIntro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Convention: The view name is the same as the action method name, so we don't need to specify the view name explicitly.
            // Action Index -> In HomeController (Controller: Home, Action: Index)
            // View: Views/Home/Index.cshtml
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
    }
}
