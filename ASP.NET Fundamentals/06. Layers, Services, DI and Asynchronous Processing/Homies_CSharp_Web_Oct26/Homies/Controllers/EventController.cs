using Homies.Data;
using Homies.Data.Models;
using Homies.Services.Core.Contracts;
using Homies.ViewModels.Event;
using Homies.ViewModels.Type;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Homies.GCommon.Constants.ApplicationConstants;

namespace Homies.Controllers
{
    public class EventController : Controller
    {
        private readonly HomiesDbContext _dbContext;

        private readonly IEventService _eventService;
        private readonly ITypeService _typeService;

        public EventController(HomiesDbContext dbContext,
            IEventService eventService,
            ITypeService typeService)
        {
            this._dbContext = dbContext;
            this._eventService = eventService;
            this._typeService = typeService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            EventInputModel eventInputModel = await this._eventService.GetEventInputModelAsync();

            return View(eventInputModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(EventInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                inputModel.Types = await this._typeService
                    .GetTypeDropdownEntriesAsync();

                return View(inputModel);
            }

            bool typeIdExists = (await this._typeService
                .GetTypeDropdownEntriesAsync())
                .Any(t => t.Id == inputModel.TypeId);

            if (!typeIdExists)
            {
                ModelState.AddModelError(nameof(EventInputModel.TypeId), "Please select a valid type!");
                inputModel.Types = await this._typeService
                    .GetTypeDropdownEntriesAsync();

                return View(inputModel);
            }

            string userId = ValidateAuthorizedUserId(GetUserId());
            await this._eventService.AddEventAsync(inputModel, userId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<EventAllViewModel> allEventsViewModel = await this._eventService
                .GetAllEventsAsync();

            return View(allEventsViewModel);
        }

        //[Authorize(Roles = "Admin")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = ValidateAuthorizedUserId(GetUserId());

            IEnumerable<EventAllViewModel> joinedEventsViewModel = await this._eventService
                .GetJoinedEventsAsync(userId);

            return View(joinedEventsViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Join(int? id)
        {
            string userId = ValidateAuthorizedUserId(GetUserId());
            bool joinSuccessful = await this._eventService.JoinAsync(id ?? 0, userId);

            if (!joinSuccessful)
            {
                return BadRequest("You cannot join this event.");
            }

            return RedirectToAction(nameof(Joined));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Leave(int? id)
        {
            string userId = ValidateAuthorizedUserId(GetUserId());
            bool leaveSuccessful = await this._eventService.LeaveAsync(id ?? 0, userId);
            if (!leaveSuccessful)
            {
                return BadRequest("You cannot leave this event.");
            }

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] int? id)
        {
            if (!id.HasValue || id <= 0)
            {
                return BadRequest("There was an issue with your request!");
            }

            EventDetailsViewModel? eventDetailsViewModel = await this._eventService
                .GetEventDetailsAsync(id ?? 0);

            if (eventDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(eventDetailsViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int? id)
        {
            try
            {
                if (!id.HasValue || id <= 0)
                {
                    return BadRequest("There was an issue with your request!");
                }

                string userId = ValidateAuthorizedUserId(GetUserId());
                EventInputModel? eventInputModel = await this._eventService
                    .GetEventForEditAsync(id ?? 0, userId);
                if (eventInputModel == null)
                {
                    return NotFound();
                }

                return View(eventInputModel);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int? id, EventInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                inputModel.Types = await this._typeService
                    .GetTypeDropdownEntriesAsync();

                return View(inputModel);
            }

            if (!id.HasValue || id <= 0)
            {
                return BadRequest("There was an issue with your request!");
            }

            try
            {
                string userId = ValidateAuthorizedUserId(GetUserId());
                bool editSuccessful = await this._eventService.EditEventAsync(id ?? 0, inputModel, userId);

                if (!editSuccessful)
                {
                    return BadRequest("You cannot edit this event.");
                }

                return RedirectToAction(nameof(All));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(nameof(inputModel.TypeId), ex.Message);
                inputModel.Types = await this._typeService
                    .GetTypeDropdownEntriesAsync();

                return View(inputModel);
            }
        }

        private string ValidateAuthorizedUserId(string? userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new InvalidOperationException("General identity error occurred - id is null in authorized area!");
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
