using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Services.Data.Book.Response
{
    public class GetUsersListedBookResponseData
    {
        public IEnumerable<string> UsersListedIds { get; set; }
            = new List<string>();
    }
}
