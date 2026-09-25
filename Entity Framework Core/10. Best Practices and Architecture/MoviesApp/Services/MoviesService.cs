using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;
using MoviesApp.DTOs.Movie;
using MoviesApp.Models;
using MoviesApp.Services.Interfaces;

namespace MoviesApp.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly MoviesAppDbContext _dbContext;

        public MoviesService(MoviesAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<GetAllMoviesDto>> GetAllAsync(int pageNumber = 1, int pageSize = 25)
        {
            IEnumerable<GetAllMoviesDto> allMovies = await this._dbContext.Movies
                .AsNoTracking()
                .OrderBy(m => m.Title)
                .ThenByDescending(m => m.ReleaseDate)
                .Select(m => new GetAllMoviesDto()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Genre = m.Genre,
                    Director = m.Director,
                    ReleaseDate = DateOnly.FromDateTime(m.ReleaseDate),
                    Duration = m.Duration,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl,
                    IsAddedInWatchlist = this._dbContext.Watchlists
                        .Any(w => w.MovieId == m.Id)
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArrayAsync();

            return allMovies;
        }

        /* Use AddMovieDto instead of Movie entity */
        public async Task AddAsync(AddNewMovieDto movieDto)
        {
            Movie newMovie = new Movie()
            {
                Title = movieDto.Title,
                Genre = movieDto.Genre,
                Director = movieDto.Director,
                ReleaseDate = movieDto.ReleaseDate.ToDateTime(new TimeOnly(0, 0, 0)),
                Duration = movieDto.Duration,
                Description = movieDto.Description,
                ImageUrl = movieDto.ImageUrl
            };

            await this._dbContext.Movies.AddAsync(newMovie);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
