using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductShop.DTOs.Export
{
    public class ExportCategoryByProductsDto
    {
        /* Validations are not required in the Export DTOs -> DB data is considered always valid */
        [JsonProperty("category")]
        public string Name { get; set; } = null!;

        [JsonProperty("productsCount")]
        public int ProductsCount { get; set; }

        [JsonProperty("averagePrice")]
        public string AveragePrice { get; set; } = null!;

        [JsonProperty("totalRevenue")]
        public string TotalRevenue { get; set; } = null!;
    }
}
