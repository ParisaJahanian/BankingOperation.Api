namespace BankingOperationsApi.Models
{
    public class FaraboomOptions
    {
        public const string SectionName = "Faraboom";

        public string DeviceId { get; set; }
        public string BaseAddress { get; set; }
        public string AppKey { get; set; }
        public int[] PermissionIds { get; set; }
        public string GrantType { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
     
    }
}
