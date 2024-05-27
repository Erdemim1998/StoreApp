using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Web.Models
{
    public class ProductViewModel
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Ürün Adı bilgisi zorunlu.")]
        [Display(Name = "Ürün Adı *")]
        public string? Name { get; set; }

        [JsonPropertyName("price")]
        [Required(ErrorMessage = "Ücreti bilgisi zorunlu.")]
        [Display(Name = "Ücreti *")]
        public float? Price { get; set; }

        [JsonPropertyName("description")]
        [Required(ErrorMessage = "Açıklama bilgisi zorunlu.")]
        [Display(Name = "Açıklama *")]
        public string? Description { get; set; }

        [JsonPropertyName("url")]
        [Required(ErrorMessage = "Url bilgisi zorunlu.")]
        [Display(Name = "Url *")]
        public string? Url { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        [JsonPropertyName("subCategoryId")]
        [Display(Name = "Alt Kategori *")]
        [Required(ErrorMessage = "Alt Kategori bilgisi zorunlu.")]
        public int SubCategoryId { get; set; }

        [ForeignKey(nameof(BrandId))]
        [JsonPropertyName("brandId")]
        [Display(Name = "Marka *")]
        [Required(ErrorMessage = "Marka bilgisi zorunlu.")]
        public int BrandId { get; set; }
    }
}
