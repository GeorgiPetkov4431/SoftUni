using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNotes.Web.ViewModels.Note
{
    public class NoteListItemViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;
    }
}
