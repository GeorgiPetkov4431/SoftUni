using GameZone.Data;
using GameZone.Data.Models;
using GameZone.ViewModels.Game;
using GameZone.ViewModels.Genre;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NuGet.Versioning;
using System.Diagnostics;
using static GameZone.Common.ApplicationConstants;

namespace GameZone.Controllers
{
    public class GameController : Controller
    {
        /* ASP.NET Controller Specifics: */
        /* Lifetime scope of Controller matches single HTTP Request-Response scope */
        /* DbContext is injecte through Constructor Injection, we should ensure that HTTP Request-Response uses a new instance of the DbContext */
        /* .AddDbContext<>() method registers DbContext as Scoped Service */
        /* Scoped Service Lifetime -> new instance lives only for the current context */
        private readonly ILogger<GameController> _logger;

        private readonly GameZoneDbContext _dbContext;

        public GameController(ILogger<GameController> logger, GameZoneDbContext dbContext)
        {
            this._logger = logger;
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult All()
        {
            IEnumerable<GameAllViewModel> gameAllViewModels = this._dbContext.Games
                .OrderByDescending(g => g.ReleasedOn)
                .ThenBy(g => g.Title)
                .ThenBy(g => g.Genre.Name)
                .Select(g => new GameAllViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    ImageUrl = g.ImageUrl,
                    Publisher = g.PublisherName,
                    ReleasedOn = g.ReleasedOn.ToString(ApplicationDateFormat),
                    GenreName = g.Genre.Name
                })
                .Take(EntitiesPerPage)
                .ToArray();

            return View(gameAllViewModels);
        }

        [HttpGet]
        public IActionResult Add()
        {
            GameFormModel inputModelAsVm = new GameFormModel()
            {
                ReleasedOn = DateTime.Today,
                Genres = LoadAllGenresDropdownItems()
            };

            return View(inputModelAsVm);
        }

        [HttpPost]
        public IActionResult Add(GameFormModel gameForm)
        {
            if (!ModelState.IsValid)
            {
                gameForm.Genres = LoadAllGenresDropdownItems();

                /* Return the same View as GET Method, but pre-filled with User Input Bound Data + Automatically filled Model Validation Errors */
                return View(gameForm);
            }

            bool genreExists = this._dbContext.Genres
                .Any(g => g.Id == gameForm.GenreId);

            if (!genreExists)
            {
                ModelState.AddModelError(nameof(GameFormModel.GenreId), "Invalid Genre is selected!");

                gameForm.Genres = LoadAllGenresDropdownItems();

                return View(gameForm);
            }

            try
            {
                Game newGame = new Game()
                {
                    Title = gameForm.Title,
                    ImageUrl = gameForm.ImageUrl,
                    Description = gameForm.Description,
                    PublisherName = gameForm.PublisherName,
                    ReleasedOn = gameForm.ReleasedOn,
                    GenreId = gameForm.GenreId
                };

                this._dbContext.Games.Add(newGame);
                this._dbContext.SaveChanges();

                TempData["Success"] = "Game added successfully!";
            }
            catch
            {
                this._logger.LogCritical("Error occurred while saving valid Game data! Check logs as soon as possible!");

                TempData["Error"] = "Unexpected error occurred while saving your data! Please try again later!";
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return BadRequest("There was an error with your request! Try again!");
            }

            GameFormModel? editingGame = this._dbContext.Games
                .Where(g => g.Id == id.Value)
                .Select(g => new GameFormModel()
                {
                    Title = g.Title,
                    ImageUrl = g.ImageUrl,
                    Description = g.Description,
                    PublisherName = g.PublisherName,
                    ReleasedOn = g.ReleasedOn,
                    GenreId = g.GenreId
                })
                .SingleOrDefault();

            if (editingGame == null)
            {
                return NotFound("Requested game was not found! Try again!");
            }

            editingGame.Genres = LoadAllGenresDropdownItems();

            return View(editingGame);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, [FromForm] GameFormModel formModel)
        {
            /* Validation in POST Add() */
            if (!ModelState.IsValid)
            {
                formModel.Genres = LoadAllGenresDropdownItems();

                /* Return the same View as GET Method, but pre-filled with User Input Bound Data + Automatically filled Model Validation Errors */
                return View(formModel);
            }

            bool genreExists = this._dbContext.Genres
                .Any(g => g.Id == formModel.GenreId);

            if (!genreExists)
            {
                ModelState.AddModelError(nameof(GameFormModel.GenreId), "Invalid Genre is selected!");
                formModel.Genres = LoadAllGenresDropdownItems();

                return View(formModel);
            }
            
            /* Validation in GET Edit() */
            if (!id.HasValue || id.Value <= 0)
            {
                return BadRequest("There was an error with your request! Try again!");
            }

            Game? editingGame = this._dbContext.Games
                .Find(id);
            if (editingGame == null)
            {
                return NotFound("Requested game was not found! Try again!");
            }

            try
            {
                editingGame.Title = formModel.Title;
                editingGame.Description = formModel.Description;
                editingGame.ImageUrl = formModel.ImageUrl;
                editingGame.PublisherName = formModel.PublisherName;
                editingGame.ReleasedOn = formModel.ReleasedOn;
                editingGame.GenreId = formModel.GenreId;

                this._dbContext.SaveChanges();

                TempData["Success"] = "Game edited successfully!";
            }
            catch
            {
                this._logger.LogCritical("Error occurred while saving valid Game data! Check logs as soon as possible!");

                TempData["Error"] = "Unexpected error occurred while saving your data! Please try again later!";
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return BadRequest("There was an error with your request! Try again!");
            }

            GameDeleteViewModel? editingGame = this._dbContext.Games
                .Select(g => new GameDeleteViewModel()
                {
                    Id = g.Id,
                    Title = g.Title
                })
                .SingleOrDefault(g => g.Id == id.Value);

            if (editingGame == null)
            {
                return NotFound("Requested game was not found! Try again!");
            }

            return View(editingGame);
        }

        [HttpPost]
        public IActionResult Delete([FromRoute] int? id, GameDeleteViewModel viewModel)
        {
            if (!id.HasValue || id.Value <= 0 || id.Value != viewModel.Id)
            {
                return BadRequest("There was an error with your request! Try again!");
            }

            Game? gameToDelete = this._dbContext.Games
                .Find(id);
            if (gameToDelete == null)
            {
                return NotFound("Requested game was not found! Try again!");
            }

            try
            {
                _dbContext.Games.Remove(gameToDelete);
                _dbContext.SaveChanges();

                TempData["Success"] = "Game deleted successfully!";
            }
            catch
            {
                this._logger.LogCritical("Error occurred while deleting valid Game data! Check logs as soon as possible!");

                TempData["Error"] = "Unexpected error occurred while deleting your game! Please try again later!";
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Details([FromRoute] int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return BadRequest("There was an error with your request! Try again!");
            }

            GameDetailsViewModel? gameDetailsViewModel = this._dbContext.Games
                .Select(g => new GameDetailsViewModel()
                {
                    Id = g.Id,
                    Title = g.Title,
                    ImageUrl = g.ImageUrl,
                    Description = g.Description,
                    Publisher = g.PublisherName,
                    ReleasedOn = g.ReleasedOn.ToString(ApplicationDateFormat),
                    GenreName = g.Genre.Name
                })
                .SingleOrDefault(g => g.Id == id.Value);

            if (gameDetailsViewModel == null)
            {
                return NotFound("Requested game was not found! Try again!");
            }

            return View(gameDetailsViewModel);
        }
 
        private IEnumerable<GenreDropdownViewModel> LoadAllGenresDropdownItems()
        {
            IEnumerable<GenreDropdownViewModel> allGenresDropdownItems = this._dbContext.Genres
                .AsNoTracking()
                .Select(g => new GenreDropdownViewModel()
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .OrderBy(g => g.Name)
                .ThenBy(g => g.Id)
                .ToArray();

            return allGenresDropdownItems;
        }
    }
}
