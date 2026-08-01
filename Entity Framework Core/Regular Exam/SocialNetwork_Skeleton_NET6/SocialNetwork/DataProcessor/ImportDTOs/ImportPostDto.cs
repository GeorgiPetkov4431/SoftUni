using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataProcessor.ImportDTOs
{
    public class ImportPostDto
    {
        [Required]
        [StringLength(300, MinimumLength = 5)]
        [JsonProperty("Content")]
        public string Content { get; set; } = null!;

        [Required]
        [JsonProperty("CreatedAt")]
        public string CreatedAt { get; set; } = null!;

        [JsonProperty("CreatorId")]
        public int CreatorId { get; set; }
    }
}
