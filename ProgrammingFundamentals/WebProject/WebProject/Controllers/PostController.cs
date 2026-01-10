using Microsoft.AspNetCore.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class PostController : Controller
    {
        private static List<PostModel> _posts = new();
        public IActionResult Index()
        {
            return View(_posts);
        }

        [HttpPost]
        public IActionResult Index(string title, string description)
        {
            PostModel post = new()
            {
                Title = title,
                Description = description
            };
            _posts.Add(post);

            return View(_posts);
        }
    }
}
