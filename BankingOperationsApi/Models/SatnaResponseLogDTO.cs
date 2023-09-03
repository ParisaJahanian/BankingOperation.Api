namespace BankingOperationsApi.Models
{
    public record SatnaResponseLogDTO(string publicRequestId, string jsonResponse,
           string satnaHttpResponseCode, string satnaRequestId, string satnaResCode);
}
