using Homies.Data;
using Homies.Data.Models;
using Homies.Services.Core.Contracts;
using Homies.ViewModels.Event;
using Homies.ViewModels.Type;
using Microsoft.EntityFrameworkCore;

namespace Homies.Services.Core.Services
{
    using static GCommon.Constants.ApplicationConstants;

    public class EventService : IEventService
    {
        private readonly HomiesDbContext _dbContext;

        private readonly ITypeService _typeService;

        public EventService(ITypeService typeService,
            HomiesDbContext dbContext)
        {
            this._typeService = typeService;
            this._dbContext = dbContext; 
        }

        public async Task<EventInputModel> GetEventInputModelAsync()
        {
            IEnumerable<TypeDropdownViewModel> typesDropdownEntries = await this._typeService
                .GetTypeDropdownEntriesAsync();
            EventInputModel eventInputModel = new EventInputModel()
            {
                Types = typesDropdownEntries
            };

            return eventInputModel;
        }

        public async Task AddEventAsync(EventInputModel inputModel, string currentUserId)
        {
            Event newEvent = new Event()
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                OrganizerId = currentUserId,
                CreatedOn = DateTime.UtcNow,
                Start = inputModel.Start,
                End = inputModel.End,
                TypeId = inputModel.TypeId
            };

            await this._dbContext.Events.AddAsync(newEvent);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventAllViewModel>> GetAllEventsAsync()
        {
            IEnumerable<EventAllViewModel> allEventsViewModel = await this._dbContext.Events
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
                .ToArrayAsync();

            return allEventsViewModel;
        }

        public async Task<IEnumerable<EventAllViewModel>> GetJoinedEventsAsync(string currentUserId)
        {
            IEnumerable<EventAllViewModel> joinedEventsViewModel = await this._dbContext.Events
                .OrderBy(e => e.Name)
                .ThenByDescending(e => e.Start)
                .Where(e => e.Participants.Any(ep => ep.HelperId == currentUserId))
                .Select(e => new EventAllViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start.ToString(DateTimeFormat),
                    TypeName = e.Type.Name,
                    OrganizerUsername = e.Organizer.UserName ?? string.Empty
                })
                .Take(EntitiesPerPage)
                .ToArrayAsync();

            return joinedEventsViewModel;
        }

        public async Task<bool> JoinAsync(int eventId, string currentUserId)
        {
            if (eventId <= 0)
            {
                return false;
            }

            Event? requestedEvent = await this._dbContext.Events
                .FindAsync(eventId);
            if (requestedEvent == null)
            {
                return false;
            }

            bool isUserOrganizerOfEvent = requestedEvent.OrganizerId == currentUserId;

            if (isUserOrganizerOfEvent)
            {
                return false;
            }

            bool userAlreadyJoined = requestedEvent.Participants
                .Any(ep => ep.HelperId == currentUserId);

            if (userAlreadyJoined)
            {
                return false;
            }

            EventParticipant newEventParticipant = new EventParticipant()
            {
                EventId = eventId,
                HelperId = currentUserId
            };

            await this._dbContext.EventsParticipants.AddAsync(newEventParticipant);
            await this._dbContext.SaveChangesAsync();

            return true;
        }
        
        public async Task<bool> LeaveAsync(int eventId, string currentUserId)
        {
            Event? eventToLeave = await this._dbContext.Events
                .FindAsync(eventId);

            if (eventToLeave == null)
            {
                return false;
            }

            
            EventParticipant? eventParticipant = await this._dbContext.EventsParticipants
                .FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.HelperId == currentUserId);

            if (eventParticipant == null)
            {
                return false;
            }

            this._dbContext.EventsParticipants.Remove(eventParticipant);
            await this._dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<EventDetailsViewModel?> GetEventDetailsAsync(int eventId)
        {
            Event? currentEvent = await this._dbContext.Events
                .Include(e => e.Organizer)
                .Include(e => e.Type)
                .FirstOrDefaultAsync(e => e.Id == eventId);
            if (currentEvent == null)
            {
                return null;
            }

            EventDetailsViewModel eventDetailsViewModel = new EventDetailsViewModel()
            {
                Id = currentEvent.Id,
                Name = currentEvent.Name,
                Description = currentEvent.Description,
                Start = currentEvent.Start.ToString(DateTimeFormat),
                End = currentEvent.End.ToString(DateTimeFormat),
                OrganizerName = currentEvent.Organizer.UserName ?? string.Empty,
                CreatedOn = currentEvent.CreatedOn.ToString(DateTimeFormat),
                TypeName = currentEvent.Type.Name
            };

            return eventDetailsViewModel;
        }

        public async Task<EventInputModel?> GetEventForEditAsync(int id, string currentUserId)
        {
            Event? eventToEdit = await this._dbContext.Events
                .FindAsync(id);

            if (eventToEdit == null)
            {
                return null;
            }

            if (eventToEdit.OrganizerId != currentUserId)
            {
                throw new UnauthorizedAccessException();
            }

            EventInputModel eventInputModel = new EventInputModel()
            {
                Name = eventToEdit.Name,
                Description = eventToEdit.Description,
                Start = eventToEdit.Start,
                End = eventToEdit.End,
                TypeId = eventToEdit.TypeId,
                Types = await this._typeService.GetTypeDropdownEntriesAsync()
            };

            return eventInputModel;
        }

        public async Task<bool> EditEventAsync(int id, EventInputModel inputModel, string currentUserId)
        {
            Event? eventToEdit = await this._dbContext.Events
                .FindAsync(id);

            if (eventToEdit == null)
            {
                return false;
            }

            if (eventToEdit.OrganizerId != currentUserId)
            {
                throw new UnauthorizedAccessException();
            }

            if (!(await this._typeService.GetTypeDropdownEntriesAsync()).Any(t => t.Id == inputModel.TypeId))
            {
                throw new ArgumentException("Type does not exist.");
            }

            eventToEdit.Name = inputModel.Name;
            eventToEdit.Description = inputModel.Description;
            eventToEdit.Start = inputModel.Start;
            eventToEdit.End = inputModel.End;
            eventToEdit.TypeId = inputModel.TypeId;

            await this._dbContext.SaveChangesAsync();

            return true;
        }
    }
}
