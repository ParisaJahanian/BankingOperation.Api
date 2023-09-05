using System.Text.Json.Serialization;

namespace BankingOperationsApi.Models
{
    public class PayaTransferReq
    {
        [JsonPropertyName("iban_number")]
        public string IbanNumber { get; set; }

        [JsonPropertyName("owner_name")]
        public string OwnerName { get; set; }

        [JsonPropertyName("Amount")]
        public string Amount { get; set; }

        [JsonPropertyName("source_deposit_number")]
        public string SourceDepositNumber { get; set; }

        [JsonPropertyName("ReceiverName")]
        public string ReceiverName { get; set; }

        [JsonPropertyName("receiver_family")]
        public string ReceiverFamily { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

}
