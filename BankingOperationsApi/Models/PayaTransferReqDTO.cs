namespace BankingOperationsApi.Models
{
    public record PayaTransferReqDTO : BasePublicLogData
    {
        public string IbanNumber { get; set; }
        public string OwnerName { get; set; }
        public string Amount { get; set; }
        public string SourceDepositNumber { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverFamily { get; set; }
        public string Description { get; set; }
    }

}
