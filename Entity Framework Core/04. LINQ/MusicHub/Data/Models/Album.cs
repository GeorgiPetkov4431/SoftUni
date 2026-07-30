using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        /* Drawback of this calculated property is that all Songs will always need to be fetched during Album fetch */
        /* If the navigation collection consists of many Entities, then business logic in Client on adding can handle this use-case */
        public decimal Price
            => this.Songs.Sum(s => s.Price);

        public int? ProducerId { get; set; }

        public virtual Producer? Producer { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
            = new HashSet<Song>();
    }
}
