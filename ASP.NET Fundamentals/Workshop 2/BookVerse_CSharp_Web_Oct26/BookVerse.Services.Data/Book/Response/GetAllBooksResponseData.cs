using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Services.Data.Book.Response
{
    public class GetAllBooksResponseData
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string GenreName { get; set; } = null!;

        public string? CoverImageUrl { get; set; }

        public string PublisherId { get; set; } = null!;


    }
}
