using IdentityNotes.Services.Contracts;
using IdentityNotes.Services.Data.Note.Request;
using IdentityNotes.Services.Data.Note.Response;
using IdentityNotes.Web.ViewModels.Note;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static IdentityNotes.Common.ApplicationConstants;

namespace IdentityNotes.Controllers
{
    public class NoteController : BaseController
    {
        private readonly INoteService _noteService;
        private readonly ILogger _logger;

        public NoteController(INoteService noteService, ILogger logger)
        {
            this._noteService = noteService;
            this._logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> MyNotes()
        {
            string? userId = GetuserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            GetUserNotesRequestData requestData = new GetUserNotesRequestData
            {
                UserId = userId,
                SkipEntries = DefaultSkipEntries, // No pagination implemented yet
                TakeEntries = DefaultTakeEntries, // No pagination implemented yet
            };
            
            try
            {
                IEnumerable<GetUserNotesResponseData> getUserNotesResponseData = await this._noteService
                .GetAllUserNotesAsync(requestData);

                IEnumerable<NoteListItemViewModel> userNotesViewModel = getUserNotesResponseData
                    .OrderByDescending(n => n.CreatedOn)
                    .ThenBy(n => n.Title)
                    .Select(n => new NoteListItemViewModel()
                    {
                        Id = n.Id,
                        Title = n.Title,
                        CreatedOn = n.CreatedOn.ToString(DateTimeFormat),
                    })
                    .ToArray();

                return View(userNotesViewModel);
            }
            catch (ArgumentException e)
            {
                this._logger.LogError(e, "ArgumentException occurred while retrieving user notes.");
                throw;
            }
        }
    }
}
