using System.Text.Json.Serialization;

namespace LineOAuthWebApp.Models
{
    public class LineUserProfile
    {

        [JsonPropertyName("userId")]
        public string UserId { get; set; } = string.Empty;

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = string.Empty;

        [JsonPropertyName("pictureUrl")]
        public string PictureUrl { get; set; } = string.Empty;

        [JsonPropertyName("statusMessage")]
        public string StatusMessage { get; set; } = string.Empty;
    }
}
