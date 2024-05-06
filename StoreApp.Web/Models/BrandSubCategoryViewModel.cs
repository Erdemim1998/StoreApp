using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StoreApp.Web.Models
{
    public class BrandSubCategoryViewModel
    {
        [JsonPropertyName("brandId")]
        [ForeignKey(nameof(BrandId))]
        public int BrandId { get; set; }

        [JsonPropertyName("subCategoryId")]
        [ForeignKey(nameof(SubCategoryId))]
        public int SubCategoryId { get; set; }
    }
}
