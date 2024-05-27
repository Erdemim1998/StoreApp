using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class BasketItem
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("price")]
        public float? Price { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [ForeignKey(nameof(BrandId))]
        [JsonPropertyName("brandId")]
        public int BrandId { get; set; }

        [JsonPropertyName("brand")]
        public Brand Brand { get; set; } = null!;

        [JsonPropertyName("count")]
        [NotMapped]
        public int Count { get; set; }
    }
}
