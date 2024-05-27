using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Data.Concrete
{
    public class Category
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Kategori Adı bilgisi zorunlu.")]
        [Display(Name = "Kategori Adı *")]
        public string? Name { get; set; }
    }
}
