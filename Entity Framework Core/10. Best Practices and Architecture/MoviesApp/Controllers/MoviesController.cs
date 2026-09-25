using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;
using MoviesApp.DTOs.Movie;
using MoviesApp.Mappers;
using MoviesApp.Models;
using MoviesApp.Services.Interfaces;
using MoviesApp.ViewModels.Movies;
using System.Linq;

using static MoviesApp.Common.ApplicationConstants;

namespace MoviesApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly MovieMapper _movieMapper;
        private readonly MoviesAppDbContext _dbContext;

        /* Constructor injection -> Instance of MoviesAppDbContext is passed from outside (ASP.NET Core) */
        /* To use it, store it locally in a field */
        public MoviesController(IMoviesService moviesService, MovieMapper movieMapper, MoviesAppDbContext dbContext)
        {
            this._moviesService = moviesService;
            this._movieMapper = movieMapper;
            this._dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            /* Always limit the maximum count of fetched rows during Db Requests from Controller */
            /* Use pagination if many rows exist */
            IEnumerable<GetAllMoviesDto> allMovies = await this._moviesService
                .GetAllAsync();

            /* Formatting should be performed in the Controller/View, not the Service */
            //IEnumerable<AllMoviesIndexViewModel> allMoviesViewModels = allMovies
            //    .Select(m => new AllMoviesIndexViewModel
            //    {
            //        Id = m.Id,
            //        Title = m.Title,
            //        Genre = m.Genre,
            //        Director = m.Director,
            //        ReleaseDate = m.ReleaseDate.ToShortDateString(),
            //        Duration = m.Duration,
            //        Description = m.Description,
            //        ImageUrl = m.ImageUrl ?? DefaultImageUrl,
            //        IsAddedInWatchlist = m.IsAddedInWatchlist
            //    })
            //    .ToArray();

            IEnumerable<AllMoviesIndexViewModel> allMoviesViewModels = this._movieMapper
                .MapToAllMoviesIndexViewModels(allMovies);

            return View(allMoviesViewModels);
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
        public async Task<IActionResult> Create(AddMovieFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AddNewMovieDto newMovieDto = this._movieMapper
                .MapToAddNewMovieDto(model);
            await this._moviesService.AddAsync(newMovieDto);

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
