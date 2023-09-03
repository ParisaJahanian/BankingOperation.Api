using System.Text.Json.Serialization;

namespace BankingOperationsApi.Models
{
    public record FaraboomLoginOutput
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public long ExpireTime { get; set; }
    }
}
