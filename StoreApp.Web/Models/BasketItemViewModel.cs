using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Web.Models
{
    public class BasketItemViewModel
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
    }
}
