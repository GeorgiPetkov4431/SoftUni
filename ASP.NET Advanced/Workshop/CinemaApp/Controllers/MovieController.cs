using CinemaApp.Services.Core.Contracts;
using CinemaApp.Services.Core.Data.Movie.Response;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static CinemaApp.GCommon.ApplicationConstants;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            this._movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<GetAllMoviesResponseModel> allMoviesResponseData = await this._movieService
                .GetAllMoviesAsync();

            IEnumerable<AllMoviesIndexViewModel> allMoviesViewModel = allMoviesResponseData
                .Select(m => new AllMoviesIndexViewModel
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Genre = m.Genre,
                    Director = m.Director,
                    ReleaseDate = m.ReleaseDate.ToString(MovieReleaseDateFormat, CultureInfo.InvariantCulture),
                    ImageUrl = m.ImageUrl ?? DefaultImageUrl
                });

            return View(allMoviesViewModel);
        }
    }
}
