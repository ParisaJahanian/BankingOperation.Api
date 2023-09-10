using System.Text.Json.Serialization;

namespace BankingOperationsApi.Models
{
    public record SatnaTransferReq 
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("source_deposit_number")]
        public string SourceDepositNumber { get; set; }

        [JsonPropertyName("receiver_name")]
        public string ReceiverName { get; set; }

        [JsonPropertyName("receiver_family")]
        public string ReceiverFamily { get; set; }

        [JsonPropertyName("destination_iban_number")]
        public string DestinationIbanNumber { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("info")]
        public string Info { get; set; }
    }
}
