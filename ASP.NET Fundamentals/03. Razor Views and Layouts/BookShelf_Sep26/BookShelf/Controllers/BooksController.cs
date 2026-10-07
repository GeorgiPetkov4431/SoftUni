using BookShelf.Data;
using BookShelf.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BookShelf.Common.ApplicationConstants;

namespace BookShelf.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookShelfDbContext _dbContext;

        public BooksController(BookShelfDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Book> allBooksWithAuthors = this._dbContext.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .OrderBy(b => b.Title)
                .ThenByDescending(b => b.Year)
                .ThenBy(b => b.Id)
                .Take(EntitiesPerPage)
                .ToArray();

            return View(allBooksWithAuthors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            /* Good design tip: Use front-end application to load all authors chunk-wise by using API */
            IEnumerable<Author> allAuthors = this._dbContext.Authors
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Country)
                .ThenBy(a => a.Id)
                .ToArray();

            ViewBag.Authors = allAuthors;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            IEnumerable<Author> allAuthors = this._dbContext.Authors
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Country)
                .ThenBy(a => a.Id)
                .ToArray();

            bool authorExists = allAuthors
                .Any(a => a.Id == book.AuthorId);

            if (!authorExists)
            {
                ModelState.AddModelError(nameof(Book.AuthorId), "Invalid author was selected!");
                ViewBag.Authors = allAuthors;

                return View(book);
            }

            try
            {
                this._dbContext.Books.Add(book);
                this._dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "General error ocurred while adding your book! Please contact our support!");
                ViewBag.Authors = allAuthors;

                return View(book);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
