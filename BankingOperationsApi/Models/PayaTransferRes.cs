using BankingOperationsApi.ErrorHandling;
using System.Text.Json.Serialization;

namespace BankingOperationsApi.Models
{
    public class PayaTransferRes : ErrorResult
    {
        [JsonPropertyName("operation_time")]
        public long OperationTime { get; set; }

        [JsonPropertyName("ref_id")]
        public string RefId { get; set; }

        [JsonPropertyName("reference_id")]
        public string ReferenceId { get; set; }

        [JsonPropertyName("source_iban_number")]
        public string SourceIbanNumber { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("transfer_status")]
        public string TransferStatus { get; set; }

        [JsonPropertyName("iban_number ")]
        public string IbanNumber { get; set; }

        [JsonPropertyName("owner_name")]
        public string OwnerName { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("transaction_status")]
        public string TransactionStatus { get; set; }

        [JsonPropertyName("instant_debit")]
        public bool InstantDebit { get; set; }


    }
}
