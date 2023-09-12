using BankingOperationsApi.ErrorHandling;
using System.Text.Json.Serialization;

namespace BankingOperationsApi.Models
{
    public class PayaBatchTransferResDTO
    {
        [JsonPropertyName("operation_time")]
        public long OperationTime { get; set; }

        [JsonPropertyName("ref_id")]
        public string RefId { get; set; }

        [JsonPropertyName("transfer_description")]
        public string TransferDeescription { get; set; }

        [JsonPropertyName("reference_id")]
        public string ReferenceId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("source_iban_number")]
        public string SourceIbanNumber { get; set; }

        [JsonPropertyName("transactions")]
        public ICollection<Transactions> Transactions { get; set; }

        [JsonPropertyName("instant_debit")]
        public bool InstantDebit { get; set; }
    }
  
}
