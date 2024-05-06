using System.Text.Json.Serialization;

namespace StoreApp.Web.Models
{
    public class AppUserRolesViewModel
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; } = null!;

        [JsonPropertyName("roleId")]
        public string RoleId { get; set; } = null!;
    }
}
