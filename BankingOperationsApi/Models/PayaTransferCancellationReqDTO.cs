using System.Text.Json.Serialization;

namespace BankingOperationsApi.Models
{
    public record PayaTransferCancellationReqDTO : BasePublicLogData
    {
        [JsonPropertyName("reference_id")]
        public string ReferenceId { get; set; }
    }
}
