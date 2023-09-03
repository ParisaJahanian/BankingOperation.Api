using BankingOperationsApi.Models;

namespace BankingOperationsApi.Data.Repositories
{
    public interface ISatnaTransferRepository
    {
        Task<string> InsertSatnaResponseLog(SatnaResponseLogDTO satnaResponseLogDTO);
        Task<string> InsertSatnaRequestLog(SatnaRequestLogDTO carTollsRequestLog);
    }
}
