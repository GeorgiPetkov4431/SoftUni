using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Services.Common.Utilities;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Services.Core.Data.Movie.Request;
using CinemaApp.Services.Core.Data.Movie.Response;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core
{
    public class MovieService : IMovieService
    {
        private readonly CinemaAppDbContext _dbContext;

        private readonly BasicModelValidator _modelValidator;

        public MovieService(CinemaAppDbContext dbContext)
        {
            this._dbContext = dbContext;

            this._modelValidator = new BasicModelValidator();
        }

        public async Task<IEnumerable<GetAllMoviesResponseModel>> GetAllMoviesAsync()
        {
            /* TODO: Implement Mapperly for auto generation of mapping code */
            IEnumerable<GetAllMoviesResponseModel> moviesResponseData = await this._dbContext.Movies
                .OrderBy(m => m.Title)
                .ThenBy(m => m.Id)
                .Select(m => new GetAllMoviesResponseModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Genre = m.Genre,
                    Director = m.Director,
                    ReleaseDate = m.ReleaseDate,
                    ImageUrl = m.ImageUrl
                })
                .ToArrayAsync();

            return moviesResponseData;
        }

        public async Task<MovieServiceStatusResponseModel> CreateMovieAsync(CreateMovieRequestData requestData)
        {
            MovieServiceStatusResponseModel response = new MovieServiceStatusResponseModel();
            bool isInputDataValid = this._modelValidator.IsValid(requestData);

            if (!isInputDataValid)
            {
                response.Status = false;
                response.ErrorMessages = this._modelValidator.ValidationResults.Select(vr => vr.ErrorMessage);
            }
            else
            {
                Movie movie = new Movie()
                {
                    Title = requestData.Title,
                    Genre = requestData.Genre,
                    Director = requestData.Director,
                    ReleaseDate = requestData.ReleaseDate,
                    Duration = requestData.Duration,
                    Description = requestData.Description,
                    ImageUrl = requestData.ImageUrl
                };

                try
                {
                    await this._dbContext.Movies.AddAsync(movie);
                    await this._dbContext.SaveChangesAsync();
                    response.Status = true;
                }
                catch (DbUpdateException ex)
                {
                    /* TODO: Log the exception details for further analysis and debugging. */

                    response.Status = false;
                    response.ErrorMessages = new List<string?>() { ex.Message };
                }
            }

            return response;
        }
    }
}
