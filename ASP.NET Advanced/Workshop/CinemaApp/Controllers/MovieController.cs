using CinemaApp.Services.Core.Contracts;
using CinemaApp.Services.Core.Data.Movie.Request;
using CinemaApp.Services.Core.Data.Movie.Response;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

using static CinemaApp.GCommon.ApplicationConstants;
using static CinemaApp.GCommon.ErrorMessages;
using static CinemaApp.GCommon.ErrorMessages.Movie;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            this._movieService = movieService;
        }

        [AllowAnonymous]
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

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] MovieFormInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            bool isReleaseDateValid = DateTime.TryParseExact(input.ReleaseDate,
                MovieReleaseDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);

            if (!isReleaseDateValid)
            {
                ModelState.AddModelError(nameof(MovieFormInputModel.ReleaseDate), ReleaseDateFormatMessage);
                return View(input);
            }

            CreateMovieRequestData createMovieRequestData = new CreateMovieRequestData
            {
                Title = input.Title,
                Genre = input.Genre,
                ReleaseDate = releaseDate,
                Duration = input.Duration,
                Director = input.Director,
                Description = input.Description,
                ImageUrl = input.ImageUrl
            };

            MovieServiceStatusResponseModel opStatusResponse = await this._movieService
                .CreateMovieAsync(createMovieRequestData);

            if (!opStatusResponse.Status)
            {
                ModelState.AddModelError(string.Empty, GeneralErrorMessage);
                return View(input);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
