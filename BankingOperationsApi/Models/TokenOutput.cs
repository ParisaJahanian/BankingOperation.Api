using System.Text.Json.Serialization;

namespace BankingOperationsApi.Models
{
    public record TokenOutput
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public long ExpireTime { get; set; }
        public string RefreshToken { get; set; }
        public string Scope { get; set; }
    }
}
