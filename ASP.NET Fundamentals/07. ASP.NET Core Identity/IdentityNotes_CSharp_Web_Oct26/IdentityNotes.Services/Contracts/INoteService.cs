using IdentityNotes.Services.Data.Note.Request;
using IdentityNotes.Services.Data.Note.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNotes.Services.Contracts
{
    public interface INoteService
    {
        /* Service Contract Definition: Service Name + Request Data + Response Data */
        Task<IEnumerable<GetUserNotesResponseData>> GetAllUserNotesAsync(GetUserNotesRequestData requestData);
    }
}
