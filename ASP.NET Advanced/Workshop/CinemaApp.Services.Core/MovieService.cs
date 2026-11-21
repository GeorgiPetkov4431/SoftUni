using CinemaApp.Data;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Services.Core.Data.Movie.Response;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core
{
    public class MovieService : IMovieService
    {
        private readonly CinemaAppDbContext _dbContext;

        public MovieService(CinemaAppDbContext dbContext)
        {
            this._dbContext = dbContext;
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
    }
}
