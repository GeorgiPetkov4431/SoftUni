using BookVerse.Data;
using BookVerse.Services.Core.Contracts;
using BookVerse.Services.Data.Genre.Response;
using Microsoft.EntityFrameworkCore;

namespace BookVerse.Services.Core
{
    public class GenreService : IGenreService
    {
        private readonly BookVerseDbContext _dbContext;

        public GenreService(BookVerseDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<GetAllGenresResponseData>> GetAllGenresAlphabeticallyAsync()
        {
            IEnumerable<GetAllGenresResponseData> allGenresResponse = await this._dbContext.Genres
                .Select(g => new GetAllGenresResponseData()
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .OrderBy(g => g.Name)
                .ThenBy(g => g.Id)
                .ToArrayAsync();

            return allGenresResponse;
        }

        public async Task<bool> GenreExistsByIdAsync(int genreId)
        {
            return await this._dbContext.Genres
                .AnyAsync(g => g.Id == genreId);
        }
    }
}
