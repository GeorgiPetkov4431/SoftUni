using Homies.Data;
using Homies.Data.Models;
using Homies.ViewModels.Event;
using Homies.ViewModels.Type;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Homies.Common.ApplicationConstants;

namespace Homies.Controllers
{
    public class EventController : Controller
    {
        private readonly HomiesDbContext _dbContext;
        //private readonly RoleManager<string> _roleManager;
        //private readonly UserManager<string> _userManager;

        public EventController(HomiesDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [Authorize]
        [HttpGet]
        public IActionResult All()
        {
            IEnumerable<EventAllViewModel> allEventsViewModel = this._dbContext.Events
                .OrderBy(e => e.Name)
                .ThenByDescending(e => e.Start)
                .Select(e => new EventAllViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start.ToString(DateTimeFormat),
                    TypeName = e.Type.Name,
                    OrganizerUsername = e.Organizer.UserName ?? string.Empty
                })
                .Take(EntitiesPerPage)
                .ToArray();

            ///* Seed required roles at corresponding page */
            //_roleManager.CreateAsync("Admin");

            ///* Add the desired user to the role (through Admin Panel in UI) */
            //_userManager.AddToRoleAsync(GetUserId()!, "Admin");

            return View(allEventsViewModel);
        }

        //[Authorize(Roles = "Admin")]
        [Authorize]
        [HttpGet]
        public IActionResult Joined()
        {
            string userId = ValidateAuthorizedUserId(GetUserId());

            IEnumerable<EventAllViewModel> joinedEventsViewModel = this._dbContext.Events
                .OrderBy(e => e.Name)
                .ThenByDescending(e => e.Start)
                .Where(e => e.Participants.Any(ep => ep.HelperId == userId))
                .Select(e => new EventAllViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start.ToString(DateTimeFormat),
                    TypeName = e.Type.Name,
                    OrganizerUsername = e.Organizer.UserName ?? string.Empty
                })
                .Take(EntitiesPerPage)
                .ToArray();

            return View(joinedEventsViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Join(int? id)
        {
            if (!id.HasValue || id <= 0)
            {
                return BadRequest("There was an issue with your request!");
            }

            Event? requestedEvent = this._dbContext.Events
                .Find(id);
            if (requestedEvent == null)
            {
                return NotFound();
            }

            string userId = ValidateAuthorizedUserId(GetUserId());
            bool isUserOrganizerOfEvent = requestedEvent.OrganizerId == userId;

            if (isUserOrganizerOfEvent)
            {
                TempData["Error"] = "You can't join events, organized by you!";

                return RedirectToAction(nameof(All));
            }

            bool userAlreadyJoined = requestedEvent.Participants
                .Any(ep => ep.HelperId == userId);

            if (!userAlreadyJoined)
            {
                EventParticipant newEventParticipant = new EventParticipant()
                {
                    EventId = id.Value,
                    HelperId = userId
                };

                this._dbContext.EventsParticipants.Add(newEventParticipant);
                this._dbContext.SaveChanges();
            }

            return RedirectToAction(nameof(Joined));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit([FromRoute] int? id)
        {
            if (!id.HasValue || id <= 0)
            {
                return BadRequest("There was an issue with your request!");
            }

            Event? requestedEvent = this._dbContext.Events
                .Find(id);
            if (requestedEvent == null)
            {
                return NotFound();
            }

            string userId = ValidateAuthorizedUserId(GetUserId());
            bool isUserOrganizerOfEvent = requestedEvent.OrganizerId == userId;

            if (!isUserOrganizerOfEvent)
            {
                TempData["Error"] = "You can edit only the events, that you are organizing!";

                return RedirectToAction(nameof(All));
            }

            IEnumerable<TypeDropdownViewModel> typeDropdownEntries = LoadTypeDropdownEntries();
            EventInputModel eventInputModel = new EventInputModel()
            {
                Name = requestedEvent.Name,
                Description = requestedEvent.Description,
                Start = requestedEvent.Start,
                End = requestedEvent.End,
                TypeId = requestedEvent.TypeId,
                Types = typeDropdownEntries
            };

            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EventInputModel inputModel)
        {
            if (!id.HasValue || id <= 0)
            {
                return BadRequest("There was an issue with your request!");
            }

            Event? requestedEvent = this._dbContext.Events
                .Find(id);
            if (requestedEvent == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                inputModel.Types = LoadTypeDropdownEntries();

                return View(inputModel);
            }

            bool typeIdExists = this._dbContext.Types
                .Any(t => t.Id == inputModel.TypeId);

            if (!typeIdExists)
            {
                ModelState.AddModelError(nameof(EventInputModel.TypeId), "Please select a valid type!");
                inputModel.Types = LoadTypeDropdownEntries();

                return View(inputModel);
            }

            string userId = ValidateAuthorizedUserId(GetUserId());
            bool isUserOrganizerOfEvent = requestedEvent.OrganizerId == userId;

            if (!isUserOrganizerOfEvent)
            {
                TempData["Error"] = "You can edit only the events, that you are organizing!";

                return RedirectToAction(nameof(All));
            }

            requestedEvent.Name = inputModel.Name;
            requestedEvent.Description = inputModel.Description;
            requestedEvent.Start = inputModel.Start;
            requestedEvent.End = inputModel.End;
            requestedEvent.TypeId = inputModel.TypeId;

            this._dbContext.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<TypeDropdownViewModel> LoadTypeDropdownEntries()
        {
            IEnumerable<TypeDropdownViewModel> typeDropdownEntries = this._dbContext.Types
                .Select(t => new TypeDropdownViewModel()
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .OrderBy(t => t.Name)
                .ToArray();

            return typeDropdownEntries;
        }

        private string ValidateAuthorizedUserId(string? userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new InvalidOperationException("General identity error occurred - id isnull in authorized area!");
            }

            return userId;
        }

        private string? GetUserId()
        {
            string? userId = null;
            Claim? idClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim != null)
            {
                userId = idClaim.Value;
            }

            return userId;
        }
    }
}
