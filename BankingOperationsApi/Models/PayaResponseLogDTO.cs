namespace BankingOperationsApi.Models
{
    public record PayaResponseLogDTO(string publicRequestId, string jsonResponse,
           string satnaHttpResponseCode, string satnaRequestId, string satnaResCode);
}
