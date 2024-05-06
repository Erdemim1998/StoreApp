using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class ProductImage
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } = null!;

        [JsonPropertyName("productId")]
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }

        [JsonPropertyName("product")]
        public Product Product { get; set; } = null!;
    }
}
