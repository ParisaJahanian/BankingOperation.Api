namespace BankingOperationsApi.Models
{
    public record PayaRequestLogDTO(string publicRequestId, string jsonRequest,
       string userId, string publicAppId, string serviceId);
}
