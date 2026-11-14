using BookVerse.Data;
using BookVerse.DataModels;
using BookVerse.Services.Core.Contracts;
using BookVerse.Services.Data.Book.Request;
using BookVerse.Services.Data.Book.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookVerse.Services.Core
{
    public class BookService : IBookService
    {
        private readonly BookVerseDbContext _dbContext;
        private readonly ILogger<BookService> _logger;

        public BookService(BookVerseDbContext dbContext, ILogger<BookService> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }

        public async Task<bool> AddBookAsync(AddNewBookRequestData requestData)
        {
            Book newBook = new Book()
            {
                Title = requestData.Title,
                Description = requestData.Description,
                CoverImageUrl = requestData.CoverImageUrl,
                PublisherId = requestData.PublisherId,
                PublishedOn = requestData.PublishedOn,
                GenreId = requestData.GenreId
            };

            try
            {
                await this._dbContext.Books.AddAsync(newBook);
                await this._dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "An error occurred while adding a new book.");
            }

            return false;
        }

        public async Task<IEnumerable<GetAllBooksResponseData>> GetAllBooksAsync()
        {
            IEnumerable<GetAllBooksResponseData> allBooksResponse = await this._dbContext.Books
                .Select(b => new GetAllBooksResponseData()
                {
                    Id = b.Id,
                    Title = b.Title,
                    GenreName = b.Genre.Name,
                    CoverImageUrl = b.CoverImageUrl,
                    PublisherId = b.PublisherId
                })
                .OrderBy(b => b.Id)
                .ThenBy(b => b.GenreName)
                .ToArrayAsync();

            return allBooksResponse;
        }

        public async Task<GetBookDetailsResponseData?> GetBookByIdAsync(int id)
        {
            Book? book = await this._dbContext.Books
                .Include(b => b.Genre)
                .SingleOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return null;
            }

            GetBookDetailsResponseData? bookDetailsResponse = null;

            if (book != null)
            {
                bookDetailsResponse = new GetBookDetailsResponseData()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    CoverImageUrl = book.CoverImageUrl,
                    PublishedOn = book.PublishedOn,
                    Genre = book.Genre.Name,
                    PublisherId = book.PublisherId
                };
            }

            return bookDetailsResponse;
        }

        public async Task<GetUsersListedBookResponseData> GetUserIdsListedBookByIdAsync(GetUsersListedBookRequestData requestData)
        {
            IEnumerable<string> userIdsListedBook = await this._dbContext.UsersBooks
                .Where(ub => ub.BookId == requestData.BookId)
                .Select(ub => ub.UserId)
                .ToArrayAsync();

            GetUsersListedBookResponseData usersListedBookResponse = new GetUsersListedBookResponseData()
            {
                UsersListedIds = userIdsListedBook
            };

            return usersListedBookResponse;
        }

        public async Task<bool> IsBookSavedAsync(IsBookSavedRequestData requestData)
        {
            bool isBookSavedByUser = await this._dbContext.UsersBooks
                .AnyAsync(b => b.BookId == requestData.BookId
                    && b.UserId.ToLower() == requestData.UserId.ToLower());

            return isBookSavedByUser;
        }

        public async Task<bool> IsBookPublisherAsync(IsBookPublisherRequestData requestData)
        {
            bool isBookPublishedByUser = await this._dbContext.Books
                .Where(b => b.Id == requestData.BookId)
                .AnyAsync(b => b.PublisherId.ToLower() == requestData.UserId.ToLower());

            return isBookPublishedByUser;
        }

        public async Task<IEnumerable<GetMyBooksResponseData>> GetMyBooksAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return new List<GetMyBooksResponseData>();
            }

            IEnumerable<GetMyBooksResponseData> myBooksResponse = await this._dbContext.UsersBooks
                .Where(ub => ub.UserId.ToLower() == userId.ToLower())
                .OrderBy(ub => ub.BookId)
                .Select(ub => ub.Book)
                .Select(b => new GetMyBooksResponseData()
                {
                    Id = b.Id,
                    Title = b.Title,
                    CoverImageUrl = b.CoverImageUrl,
                    Genre = b.Genre.Name
                })
                .OrderBy(b => b.Title)
                .ThenBy(b => b.Genre)
                .ToArrayAsync();

            return myBooksResponse;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool bookExists = await this._dbContext.Books
                .AnyAsync(b => b.Id == id);

            return bookExists;
        }

        public async Task<bool> AddToMyBooksAsync(AddToMyBooksRequestData requestData)
        {
            bool bookExists = await this.ExistsByIdAsync(requestData.BookId);
            if (!bookExists)
            {
                this._logger.LogWarning("Book with ID {BookId} does not exist.", requestData.BookId);
                return false;
            }

            bool isBookPublisher = await this.IsBookPublisherAsync(new IsBookPublisherRequestData()
            {
                BookId = requestData.BookId,
                UserId = requestData.UserId
            });
            if (!isBookPublisher)
            {
                this._logger.LogWarning("User {UserId} is not the publisher of book with ID {BookId}. Cannot add the book to My Books!", requestData.UserId, requestData.BookId);
                return false;
            }

            bool isBookAlreadySaved = await this.IsBookSavedAsync(new IsBookSavedRequestData()
            {
                BookId = requestData.BookId,
                UserId = requestData.UserId
            });

            if (!isBookAlreadySaved)
            {
                UserBook userBook = new UserBook()
                {
                    BookId = requestData.BookId,
                    UserId = requestData.UserId
                };

                try
                {
                    await this._dbContext.UsersBooks.AddAsync(userBook);
                    await this._dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    this._logger.LogError(e, "An error occurred while adding the book with ID {BookId} to My Books for user {UserId}.", requestData.BookId, requestData.UserId);
                }

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
