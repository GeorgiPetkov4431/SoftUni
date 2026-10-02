using Microsoft.AspNetCore.Mvc;

namespace MvcIntro.Controllers
{
    public class ProductsController : Controller
    {
        private static readonly IEnumerable<string> products = new List<string>
        {
            "Laptops",
            "Gaming Consoles",
            "TVs"
        };

        public IActionResult Index()
        {
            ViewData["Products"] = products;

            return this.View();
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.BadRequest("Something was wrong with your request. Please try again!");
            }
            if (id <= 0)
            {
                return this.NotFound("Product not found.");
            }

            return this.Ok($"Product details: {id}");
        }
    }
}
