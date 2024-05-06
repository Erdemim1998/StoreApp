using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StoreApp.Data.Concrete
{
    public class BrandSubCategory
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("brandId")]
        [ForeignKey(nameof(BrandId))]
        public int BrandId { get; set; }

        [JsonPropertyName("brand")]
        public Brand Brand { get; set; } = null!;

        [JsonPropertyName("subCategoryId")]
        [ForeignKey(nameof(SubCategoryId))]
        public int SubCategoryId { get; set; }

        [JsonPropertyName("subCategory")]
        public SubCategory SubCategory { get; set; } = null!;
    }
}
