using BookShelf.Data;
using BookShelf.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BookShelf.Common.ApplicationConstants;

namespace BookShelf.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly BookShelfDbContext _dbContext;

        public AuthorsController(BookShelfDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /* Default Controller Route Convention:
         * Path: {Controller}/{Action} -> GET /Authors/Index
         * View Discovery Default Convention -> View is searced in the same path as Path from HTTP Request
         * Views are always searched in Views folder!
         * 0. If explicit View name is passed, then View Discovery will search for the view with the explicit name
         * 1. View is searched in folder named as the Controller and view name should equal to action name
         * -> Views/Authors/Index.cshtml
         * 2. If view is not found based on the above convention, View Discovery searches for View with the name of the action in the Shared folder
         * 
         */
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Author> authors = this._dbContext.Authors
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Country)
                .ThenBy(a => a.Id)
                .Take(EntitiesPerPage)
                .ToArray();

            /* Typed data passing to View */
            return View(authors);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue || id <= 0)
            {
                return BadRequest("There was something wrong with your request! Please try again!");
            }

            Author? author = this._dbContext.Authors
                .AsNoTracking()
                .Include(a => a.Books)
                .SingleOrDefault(a => a.Id == id);

            if (author == null)
            {
                // Author not found
                return NotFound("Author could not be found! Please try again later!");
            }

            return View(author);
        }
    }
}
