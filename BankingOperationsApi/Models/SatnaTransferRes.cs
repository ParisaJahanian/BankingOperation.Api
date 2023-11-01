using BankingOperationsApi.ErrorHandling;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BankingOperationsApi.Models
{
    public class SatnaTransferRes  : ErrorResult
    {
        [JsonPropertyName("operation_time")]
        public long OperationTime { get; set; }

        [JsonPropertyName("ref_id")]
        public string RefId { get; set; }

        [JsonPropertyName("balance")]
        public long Balance { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("instant_debit")]
        public bool InstantDebit { get; set; }
    }
}
