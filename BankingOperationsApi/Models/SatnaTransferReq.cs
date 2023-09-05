namespace BankingOperationsApi.Models
{
    public record SatnaTransferReq
    {
        public int Amount { get; set; }
        public int SourceDepositNumber { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverFamily { get; set; }
        public string DestinationIbanNumber { get; set; }
        public string Description { get; set; }
        public string Info { get; set; }
    }
}
