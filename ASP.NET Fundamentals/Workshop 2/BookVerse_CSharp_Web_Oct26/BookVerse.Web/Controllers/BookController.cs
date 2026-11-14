using BookVerse.Services.Core.Contracts;
using BookVerse.Services.Data.Book.Request;
using BookVerse.Services.Data.Book.Response;
using BookVerse.Services.Data.Genre.Response;
using BookVerse.ViewModels.Book;
using BookVerse.ViewModels.Genre;
using BookVerse.ViewModels.ReadingList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static BookVerse.GCommon.ApplicationConstants;

namespace BookVerse.Web.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;

        private readonly IUserStore<IdentityUser> _userStore;

        public BookController(IBookService bookService,
            IGenreService genreService,
            IUserStore<IdentityUser> userStore)
        {

            this._bookService = bookService;
            this._genreService = genreService;
            this._userStore = userStore;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = GetUserId();

            IEnumerable<GetAllBooksResponseData> allBooksResponseData = await this._bookService
                .GetAllBooksAsync();

            ICollection<BookIndexViewModel> allBooksViewModelList
                = new List<BookIndexViewModel>();

            foreach (GetAllBooksResponseData bookData in allBooksResponseData)
            {
                GetUsersListedBookRequestData usersListedBookRequestData = new GetUsersListedBookRequestData()
                {
                    BookId = bookData.Id
                };

                GetUsersListedBookResponseData usersListedBookData = await this._bookService
                    .GetUserIdsListedBookByIdAsync(usersListedBookRequestData);

                BookIndexViewModel bookViewModel = new BookIndexViewModel()
                {
                    Id = bookData.Id,
                    Title = bookData.Title,
                    CoverImageUrl = bookData.CoverImageUrl ?? DefaultCoverImageUrl,
                    Genre = bookData.GenreName,
                    IsPublisher = bookData.PublisherId.ToLower() == userId?.ToLower(),
                    SavedCount = usersListedBookData.UsersListedIds.Count(),
                    IsSaved = usersListedBookData.UsersListedIds
                        .Any(id => id.ToLowerInvariant() == userId?.ToLowerInvariant())
                };

                allBooksViewModelList.Add(bookViewModel);
            }

            return View(allBooksViewModelList);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest("There was an error with your request! Try again!");
            }

            GetBookDetailsResponseData? bookDetailsResponseData = await this._bookService
                .GetBookByIdAsync(id);

            if (bookDetailsResponseData == null)
            {
                return NotFound("The book you are looking for does not exist!");
            }

            string userId = GetUserId()!;
            string publisherName = UnknownUsername;

            bool isBookPublishedByUser = false;
            bool isBookSavedByUser = false;

            if (userId != null)
            {
                IdentityUser? identityUser = await this._userStore
                .FindByIdAsync(userId, CancellationToken.None);

                if (identityUser == null)
                {
                    return Unauthorized("You must be logged in to view this book's details!");
                }

                publisherName = identityUser.UserName ?? UnknownUsername;
                isBookSavedByUser = await this._bookService
                    .IsBookSavedAsync(new IsBookSavedRequestData()
                    {
                        BookId = id,
                        UserId = userId
                    });

                isBookPublishedByUser = bookDetailsResponseData.PublisherId.ToLower() == userId.ToLower();
            }

            BookDetailsViewModel bookDetailsViewModel = new BookDetailsViewModel()
            {
                Id = bookDetailsResponseData.Id,
                Title = bookDetailsResponseData.Title,
                Description = bookDetailsResponseData.Description,
                CoverImageUrl = bookDetailsResponseData.CoverImageUrl ?? DefaultCoverImageUrl,
                PublishedOn = bookDetailsResponseData.PublishedOn.ToString(DefaultPublishedDateFormat),
                Genre = bookDetailsResponseData.Genre,
                PublisherName = publisherName,
                IsPublisher = isBookPublishedByUser,
                IsSaved = isBookSavedByUser
            };

            return View(bookDetailsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            BookCreateInputModel inputModel = new BookCreateInputModel()
            {
                PublishedOn = DateTime.Today,
                Genres = await GetAllGenresDropdownListAsync()
            };

            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                inputModel.Genres = await GetAllGenresDropdownListAsync();
                return View(inputModel);
            }

            if (inputModel.GenreId <= 0)
            {
                ModelState.AddModelError(nameof(inputModel.GenreId), "Please select a valid Genre!");
                inputModel.Genres = await GetAllGenresDropdownListAsync();

                return View(inputModel);
            }

            bool isGenreValid = await this._genreService
                .GenreExistsByIdAsync(inputModel.GenreId);

            if (!isGenreValid)
            {
                return BadRequest("There was an error with your request! Try again!");
            }

            AddNewBookRequestData newBookRequestData = new AddNewBookRequestData()
            {
                Title = inputModel.Title,
                Description = inputModel.Description,
                CoverImageUrl = inputModel.CoverImageUrl,
                PublishedOn = inputModel.PublishedOn,
                GenreId = inputModel.GenreId,
                PublisherId = GetUserId()!
            };

            bool isBookCreated = await this._bookService
                .AddBookAsync(newBookRequestData);

            if (!isBookCreated)
            {
                ModelState.AddModelError(string.Empty, "There was an unexpected error with your request! Please contact us.");
                inputModel.Genres = await GetAllGenresDropdownListAsync();

                return View(inputModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> MyBooks()
        {
            string userId = GetUserId()!;

            IEnumerable<GetMyBooksResponseData> myBooksResponseData = await this._bookService
                .GetMyBooksAsync(userId);

            IEnumerable<BookMyBooksViewModel> myBooksViewModels = myBooksResponseData
                .Select(b => new BookMyBooksViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    CoverImageUrl = b.CoverImageUrl ?? DefaultCoverImageUrl,
                    Genre = b.Genre
                })
                .ToArray();

            return View(myBooksViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> AddToMyBooks([FromForm] int id, [FromQuery] string? returnUrl)
        {
            returnUrl ??= Url.Action(nameof(Index), "Book");

            bool bookExists = await this._bookService.ExistsByIdAsync(id);
            if (!bookExists)
            {
                return NotFound("The book you are trying to add does not exist!");
            }

            string userId = GetUserId()!;

            bool isBookPublishedByUser = await this._bookService
                .IsBookPublisherAsync(new IsBookPublisherRequestData()
                {
                    BookId = id,
                    UserId = userId
                });

            if (isBookPublishedByUser)
            {
                return BadRequest("You cannot add a book that you published to your reading list!");
            }

            AddToMyBooksRequestData addToMyBooksRequestData = new AddToMyBooksRequestData()
            {
                BookId = id,
                UserId = userId
            };
            bool bookAdded = await this._bookService.AddToMyBooksAsync(addToMyBooksRequestData);
            if (!bookAdded)
            {
                return BadRequest("There was an error with your request! Try again!");
            }

            return Redirect(returnUrl!);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookEditViewModel model)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            return Ok();
        }

        private async Task<IEnumerable<GenreViewModel>> GetAllGenresDropdownListAsync()
        {
            IEnumerable<GetAllGenresResponseData> allGenresResponseData = await this._genreService
                .GetAllGenresAlphabeticallyAsync();

            IEnumerable<GenreViewModel> allGenresViewModel = allGenresResponseData
                .Select(g => new GenreViewModel()
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToArray();

            return allGenresViewModel;
        }
    }
}
