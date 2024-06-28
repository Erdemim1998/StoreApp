using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StoreApp.Data.Concrete
{
    public class Product
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("ml1Name")]
        public string? Ml1Name { get; set; }

        [JsonPropertyName("ml2Name")]
        public string? Ml2Name { get; set; }

        [JsonPropertyName("price")]
        public float? Price { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        [JsonPropertyName("subCategoryId")]
        public int SubCategoryId { get; set; }

        [JsonPropertyName("subCategory")]
        public SubCategory SubCategory { get; set; } = null!;

        [ForeignKey(nameof(BrandId))]
        [JsonPropertyName("brandId")]
        public int BrandId { get; set; }

        [JsonPropertyName("brand")]
        public Brand Brand { get; set; } = null!;
    }
}
