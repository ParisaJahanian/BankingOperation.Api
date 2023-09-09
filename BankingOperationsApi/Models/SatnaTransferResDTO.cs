using System.Text.Json.Serialization;

namespace BankingOperationsApi.Models
{
    public class SatnaTransferResDTO
    {
        [JsonPropertyName("operation_time")]
        public long OperationTime { get; set; }

        [JsonPropertyName("ref_id")]
        public string RefId { get; set; }

        [JsonPropertyName("balance")]
        public string Balance { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("instant_debit")]
        public bool InstantDebit { get; set; }
    }
}
