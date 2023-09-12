namespace BankingOperationsApi.Models
{
    public class FaraboomOptions
    {
        public const string SectionName = "Faraboom";

        public string DeviceId { get; set; }
        public string BaseAddress { get; set; }
        public string AppKey { get; set; }
        public string GrantType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TokenId { get; set; }
        public string BankId { get; set; }
        public string Cookie { get; set; }
        public string TokenUrl { get; set; }
        public string SatnaTransferUrl { get; set; }
        public string PayaTransferUrl { get; set; }
        public string PayaBatchTransferUrl { get; set; }
        public string PayaCancelUrl { get; set; }
        public string Authorization { get; set; }

    }
}
