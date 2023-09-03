using BankingOperationsApi.Models;

namespace BankingOperationsApi.Data.Repositories
{
    public interface IPayaTransferRepository
    {
        Task<string> InsertPayaResponseLog(PayaResponseLogDTO payaResponseLogDTO);
        Task<string> InsertPayaRequestLog(PayaRequestLogDTO payaRequestLogDTO);
    }
}
