using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Web.Models
{
    public class ProductImageViewModel
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } = null!;

        [JsonPropertyName("productId")]
        [ForeignKey(nameof(ProductId))]
        [Display(Name = "Ürün")]
        public int ProductId { get; set; }
    }
}
