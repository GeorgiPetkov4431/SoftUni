using CinemaApp.Services.Core.Data.Movie.Response;

namespace CinemaApp.Services.Core.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<GetAllMoviesResponseModel>> GetAllMoviesAsync();
    }
}
