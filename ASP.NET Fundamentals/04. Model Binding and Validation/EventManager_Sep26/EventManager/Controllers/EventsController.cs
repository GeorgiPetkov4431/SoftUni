using EventManager.Data;
using EventManager.Data.Models;
using EventManager.ViewModels.Category;
using EventManager.ViewModels.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EventManager.Common.ApplicationConstants;

namespace EventManager.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventManagerDbContext _dbContext;

        public EventsController(EventManagerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string? title)
        {
            IQueryable<EventIndexViewModel> allEventsQuery = this._dbContext.Events
                .OrderByDescending(e => e.StartDate)
                .ThenBy(e => e.EndDate)
                .ThenBy(e => e.Title)
                .ThenByDescending(e => e.MaxParticipants)
                .Select(e => new EventIndexViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    CategoryName = e.Category.Name,
                    Description = e.Description,
                    StartDate = e.StartDate.ToString(DateTimeFormat),
                    EndDate = e.EndDate.ToString(DateTimeFormat),
                    CurrentParticipants = e.Registrations.Count(),
                    MaxParticipants = e.MaxParticipants
                });

            if (!string.IsNullOrWhiteSpace(title))
            {
                allEventsQuery = allEventsQuery
                    .Where(e => EF.Functions.Like(e.Title, $"%{title}%"));
            }

            IEnumerable<EventIndexViewModel> allEventsViewModels = allEventsQuery
                .Take(EntitiesPerPage)
                .ToArray();

            ViewData["AllCategories"] = LoadAllCategoriesDropdownViewModels();

            return View(allEventsQuery);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<CategoryDropdownViewModel> allCategoriesDropdownVms
                = LoadAllCategoriesDropdownViewModels();

            EventFormModel eventFormModel = new EventFormModel()
            {
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1),
                AllCategories = allCategoriesDropdownVms
            };

            return View(eventFormModel);
        }

        [HttpPost]
        public IActionResult Create(EventFormModel eventForm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid event data was detected! Please try again.");
                eventForm.AllCategories = LoadAllCategoriesDropdownViewModels();

                return View(eventForm);
            }

            bool categoryValid = this._dbContext.Categories
                .Any(c => c.Id == eventForm.CategoryId);

            if (!categoryValid)
            {
                ModelState.AddModelError(nameof(EventFormModel.CategoryId), "Invalid category is selected!");
                eventForm.AllCategories = LoadAllCategoriesDropdownViewModels();

                return View(eventForm);
            }

            try
            {
                Event eventToPersist = new Event()
                {
                    Title = eventForm.Title,
                    Description = eventForm.Description,
                    StartDate = eventForm.StartDate,
                    EndDate = eventForm.EndDate,
                    MaxParticipants = eventForm.MaxParticipants,
                    CategoryId = eventForm.CategoryId
                };

                this._dbContext.Events.Add(eventToPersist);
                this._dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home"); 
            }

            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<CategoryDropdownViewModel> LoadAllCategoriesDropdownViewModels()
        {
            return this._dbContext.Categories
                .Select(c => new CategoryDropdownViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ThenBy(c => c.Id)
                .ToArray();
        }
    }
}
