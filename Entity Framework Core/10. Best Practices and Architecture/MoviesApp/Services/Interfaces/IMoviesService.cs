using MoviesApp.DTOs.Movie;
using MoviesApp.Models;

namespace MoviesApp.Services.Interfaces
{
    public interface IMoviesService
    {
        /* Usually, Services should never be coupled to the Application ViewModels! */
        /* In the current application architecture, there is no dedicated Service layer and this is done for demonstration puroses. */
        /* Ideally, a DTO should be used to define contract for the Service users */
        Task<IEnumerable<GetAllMoviesDto>> GetAllAsync(int pageNumber = 1, int pageSize = 25);

        Task AddAsync(AddNewMovieDto movieDto);

        Task<Movie?> GetByIdAsync(int id);

        Task UpdateAsync(Movie movie);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}
