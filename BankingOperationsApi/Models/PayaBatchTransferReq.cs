using System.Text.Json.Serialization;

namespace BankingOperationsApi.Models
{

    public record PayaBatchTransferReq 
    {
        [JsonPropertyName("transfer_description")]
        public string TransferDescription { get; set; }

        [JsonPropertyName("source_deposit_number")]
        public string SourceDepositNumber { get; set; }

        [JsonPropertyName("ignore_error")]
        public bool IgnoreError { get; set; }

        [JsonPropertyName("transactions")]
        public List<Transaction> Transactions { get; set; }
    }
   

}
