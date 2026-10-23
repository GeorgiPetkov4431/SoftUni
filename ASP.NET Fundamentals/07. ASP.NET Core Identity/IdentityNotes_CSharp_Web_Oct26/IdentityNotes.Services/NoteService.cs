using IdentityNotes.Data;
using IdentityNotes.Services.Contracts;
using IdentityNotes.Services.Data.Note.Request;
using IdentityNotes.Services.Data.Note.Response;
using Microsoft.EntityFrameworkCore;

namespace IdentityNotes.Services
{
    public class NoteService : INoteService
    {
        private readonly IdentityNotesDbContext _dbContext;

        /* Dependency Injection works in separate class libraries */
        public NoteService(IdentityNotesDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<GetUserNotesResponseData>> GetAllUserNotesAsync(GetUserNotesRequestData requestData)
        {
            if (string.IsNullOrWhiteSpace(requestData.UserId))
            {
                throw new ArgumentException("UserId cannot be null or empty.", nameof(requestData.UserId));
            }

            IEnumerable<GetUserNotesResponseData> userNotes = await this._dbContext.Notes
                .Where(note => note.UserId.ToLower() == requestData.UserId.ToLower())
                .Select(note => new GetUserNotesResponseData()
                {
                    Id = note.Id,
                    Title = note.Title,
                    CreatedOn = note.CreatedOn
                })
                .OrderBy(note => note.Id)
                .Skip(requestData.SkipEntries)
                .Take(requestData.TakeEntries)
                .ToArrayAsync();

            return userNotes;
        }
    }
}
