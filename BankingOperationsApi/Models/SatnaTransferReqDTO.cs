namespace BankingOperationsApi.Models
{
    public record SatnaTransferReqDTO : BasePublicLogData
    {
        public int Amount { get; set; }
        public int SourceDepositNumber { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverFamily { get; set; }
        public string DestinationIbanNumber { get; set; }
        public string description { get; set; }
        public string info { get; set; }
    }
}
