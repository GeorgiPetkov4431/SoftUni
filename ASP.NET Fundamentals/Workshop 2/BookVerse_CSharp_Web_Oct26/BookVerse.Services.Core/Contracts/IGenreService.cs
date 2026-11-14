using BookVerse.Services.Data.Genre.Response;

namespace BookVerse.Services.Core.Contracts
{
    public interface IGenreService
    {
        Task<IEnumerable<GetAllGenresResponseData>> GetAllGenresAlphabeticallyAsync();

        Task<bool> GenreExistsByIdAsync(int genreId);
    }
}
