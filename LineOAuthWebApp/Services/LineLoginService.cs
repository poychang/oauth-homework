using LineOAuthWebApp.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LineOAuthWebApp.Services
{
    public class LineLoginService
    {
        private HttpClient _httpClient;

        public LineLoginService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(LineLoginService));
        }

        public string LineLoginUrl(string clientId, string redirectUri, string state)
        {
            return $"https://access.line.me/oauth2/v2.1/authorize"
                + $"?response_type=code"
                + $"&client_id={clientId}"
                + $"&redirect_uri={redirectUri}"
                + $"&state={state}"
                + $"&scope=openid%20profile";
        }

        public async Task<string> FetchLineLoginAccessTokenAsync(string code, string clientId, string clientSecret, string redirectUri)
        {
            var endpoint = "https://api.line.me/oauth2/v2.1/token";
            var payload = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "redirect_uri", redirectUri }
            });
            var response = await _httpClient.PostAsync(endpoint, payload);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();

            return JsonSerializer.Deserialize<LineLoginAccessToken>(responseStream)?.AccessToken ?? string.Empty;
        }

        public async Task<LineUserProfile> FetchLineUserProfileAsync(string accessToken)
        {
            var endpoint = "https://api.line.me/v2/profile";
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();

            return JsonSerializer.Deserialize<LineUserProfile>(responseStream) ?? default!;
        }

        public async Task RevokeAccessTokenAsync(string accessToken, string clientId, string clientSecret, Action postAction = default!)
        {
            var endpoint = "https://api.line.me/oauth2/v2.1/revoke";
            var response = await _httpClient.PostAsync(endpoint, new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "access_token", accessToken },
                { "client_id", clientId },
                { "client_secret", clientSecret }
            }));
            response.EnsureSuccessStatusCode();

            if (postAction is not null) postAction();
        }
    }
}
