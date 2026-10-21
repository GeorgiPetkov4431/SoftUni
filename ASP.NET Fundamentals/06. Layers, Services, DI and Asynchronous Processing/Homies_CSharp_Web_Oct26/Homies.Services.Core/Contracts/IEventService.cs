using Homies.Data.Models;
using Homies.ViewModels.Event;

namespace Homies.Services.Core.Contracts
{
    public interface IEventService
    {
        Task<EventInputModel> GetEventInputModelAsync();

        Task AddEventAsync(EventInputModel eventInputModel, string currentUserId);

        Task<IEnumerable<EventAllViewModel>> GetAllEventsAsync();

        Task<IEnumerable<EventAllViewModel>> GetJoinedEventsAsync(string currentUserId);

        Task<bool> JoinAsync(int eventId, string currentUserId);

        Task<bool> LeaveAsync(int eventId, string currentUserId);

        Task<EventDetailsViewModel?> GetEventDetailsAsync(int eventId);

        Task<EventInputModel?> GetEventForEditAsync(int id, string currentUserId);

        Task<bool> EditEventAsync(int id, EventInputModel inputModel, string currentUserId);
    }
}
