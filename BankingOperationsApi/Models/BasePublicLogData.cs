using System.Text.Json;

namespace BankingOperationsApi.Models
{
    public record BasePublicLogData
    {
        public PublicLogData PublicLogData { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this); ;
        }
    }

    public class PublicLogData
    {
        public string UserId { get; set; }
        public string PublicAppId { get; set; }
        public string ServiceId { get; set; }
        public string PublicReqId { get; set; }
    }
}
