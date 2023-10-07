using System.Text.Json.Serialization;

namespace BankingOperationsApi.Models
{
    public record PayaBatchTransferReqDTO : BasePublicLogData
    {
        public string TransferDescription { get; set; }
        public string SourceDepositNumber { get; set; }
        public bool IgnoreError { get; set; }
        public Transaction[] Transactions { get; set; }
    }
    public class Transaction
    {
        [JsonPropertyName("iban_number")]
        public string IbanNumber { get; set; }

        [JsonPropertyName("owner_name")]
        public string OwnerName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("factor_number")]
        public string FactorNumber { get; set; }
    }
}
