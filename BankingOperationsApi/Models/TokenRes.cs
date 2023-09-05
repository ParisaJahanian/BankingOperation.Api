using BankingOperationsApi.ErrorHandling;
using System.DirectoryServices.Protocols;
using System.Text.Json.Serialization;

namespace BankingOperationsApi.Models
{
    public class TokenRes : ErrorResult
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public long ExpireTime { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }
}
