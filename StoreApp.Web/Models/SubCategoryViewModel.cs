using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StoreApp.Web.Models
{
    public class SubCategoryViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required]
        [Display(Name = "Alt Kategori Adı")]
        public string? Name { get; set; }

        [JsonPropertyName("url")]
        [Required]
        public string? Url { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [JsonPropertyName("categoryId")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
    }
}
