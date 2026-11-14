using BookVerse.Services.Data.Book.Request;
using BookVerse.Services.Data.Book.Response;
using BookVerse.ViewModels.Book;
using BookVerse.ViewModels.Genre;
using BookVerse.ViewModels.ReadingList;

namespace BookVerse.Services.Core.Contracts
{
    public interface IBookService
    {
        Task<bool> AddBookAsync(AddNewBookRequestData requestData);

        Task<bool> AddToMyBooksAsync(AddToMyBooksRequestData requestData);

        //Task DeleteBookAsync(int id, string userId);

        Task<GetUsersListedBookResponseData> GetUserIdsListedBookByIdAsync(GetUsersListedBookRequestData requestData);

        Task<IEnumerable<GetAllBooksResponseData>> GetAllBooksAsync();

        Task<GetBookDetailsResponseData?> GetBookByIdAsync(int id);

        Task<bool> IsBookSavedAsync(IsBookSavedRequestData requestData);

        Task<bool> IsBookPublisherAsync(IsBookPublisherRequestData requestData);

        Task<IEnumerable<GetMyBooksResponseData>> GetMyBooksAsync(string userId);

        Task<bool> ExistsByIdAsync(int id);

        //Task RemoveFromMyBooksAsync(int id, string userId);

        //Task<BookEditViewModel> GetBookForEditAsync(int id, string userId);

        //Task EditBookAsync(BookEditViewModel model, string userId);

        //Task<IEnumerable<GenreViewModel>> GetAllGenresAsync();


        //Task<BookDeleteViewModel> GetBookDeleteDetailsAsync(int id, string userId);
    }
}
