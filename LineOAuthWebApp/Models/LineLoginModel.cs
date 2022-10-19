using System.Text.Json.Serialization;

namespace LineOAuthWebApp.Models
{
    public class LineLoginAccessToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; } = 0;

        [JsonPropertyName("id_token")]
        public string IdToken { get; set; } = string.Empty;

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;

        [JsonPropertyName("scope")]
        public string Scope { get; set; } = string.Empty;

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = string.Empty;
    }

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
