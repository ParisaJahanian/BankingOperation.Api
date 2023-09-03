namespace BankingOperationsApi.Models
{
    public record SatnaRequestLogDTO(string publicRequestId, string jsonRequest,
       string userId, string publicAppId, string serviceId);
}
