using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.Services.Interfaces;
using MoviesApp.ViewModels.Movies;
using System.Linq;

namespace MoviesApp.Controllers
{
    public class MoviesController : Controller
    {
        private const string DefaultImageUrl =
            "https://img.freepik.com/free-vector/cinema-film-production-realistic-transparent-composition-with-isolated-image-clapper-with-empty-fields-vector-illustration_1284-66163.jpg?semt=ais_incoming&w=740&q=80";

        private readonly IMoviesService _moviesService;
        private readonly MoviesAppDbContext _dbContext;

        /* Constructor injection -> Instance of MoviesAppDbContext is passed from outside (ASP.NET Core) */
        /* To use it, store it locally in a field */
        public MoviesController(IMoviesService moviesService, MoviesAppDbContext dbContext)
        {
            this._moviesService = moviesService;
            this._dbContext = dbContext;
        }

        public IActionResult Index()
        {
            /* Always limit the maximum count of fetched rows during Db Requests from Controller */
            /* Use pagination if many rows exist */
            IEnumerable<AllMoviesIndexViewModel> allMovies = this._dbContext.Movies
                .AsNoTracking()
                .OrderBy(m => m.Title)
                .ThenByDescending(m => m.ReleaseDate)
                .Select(m => new AllMoviesIndexViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Genre = m.Genre,
                    Director = m.Director,
                    ReleaseDate = m.ReleaseDate.ToShortDateString(),
                    Duration = m.Duration,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl ?? DefaultImageUrl,
                    IsAddedInWatchlist = this._dbContext.Watchlists
                        .Any(w => w.MovieId == m.Id)
                })
                .Take(25)
                .ToArray();

            return View(allMovies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            AddMovieFormModel model = new AddMovieFormModel
            {
                ReleaseDate = DateTime.Today
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(AddMovieFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Movie newMovie = new Movie()
            {
                Title = model.Title,
                Genre = model.Genre,
                Director = model.Director,
                Duration = model.Duration,
                ReleaseDate = model.ReleaseDate,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };

            this._dbContext.Movies.Add(newMovie);
            this._dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Movie? movie = this._dbContext.Movies
               .Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            var model = new MovieDetailsViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                Director = movie.Director,
                Duration = movie.Duration,
                ReleaseDate = movie.ReleaseDate,
                Description = movie.Description,
                ImageUrl = movie.ImageUrl ?? DefaultImageUrl
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Movie? movie = this._dbContext.Movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            var model = new EditMovieFormModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                Director = movie.Director,
                Duration = movie.Duration,
                ReleaseDate = movie.ReleaseDate,
                Description = movie.Description,
                ImageUrl = movie.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditMovieFormModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Movie? movie = this._dbContext.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            movie.Title = model.Title;
            movie.Genre = model.Genre;
            movie.Director = model.Director;
            movie.Duration = model.Duration;
            movie.ReleaseDate = model.ReleaseDate;
            movie.Description = model.Description;
            movie.ImageUrl = model.ImageUrl;

            this._dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Movie? movie = this._dbContext.Movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            DeleteMovieViewModel deleteViewModel = new DeleteMovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                Director = movie.Director,
                ReleaseDate = movie.ReleaseDate,
                ImageUrl = movie.ImageUrl ?? DefaultImageUrl
            };

            return View(deleteViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Movie? movie = this._dbContext.Movies.Find(id);

            if (movie != null)
            {
                this._dbContext.Movies.Remove(movie);
                this._dbContext.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
