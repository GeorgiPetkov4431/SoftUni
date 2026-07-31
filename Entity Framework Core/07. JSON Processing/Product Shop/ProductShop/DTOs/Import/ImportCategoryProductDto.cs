using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductShop.DTOs.Import
{
    public class ImportCategoryProductDto
    {
        [JsonRequired]
        [JsonProperty("CategoryId")]
        public int CategoryId { get; set; }

        [JsonRequired]
        [JsonProperty("ProductId")]
        public int ProductId { get; set; }
    }
}
