using BankingOperationsApi.Models;

namespace BankingOperationsApi.Data.Repositories
{
    public class PayaTransferRepository : IPayaTransferRepository
    {
        Task<string> IPayaTransferRepository.InsertPayaRequestLog(PayaRequestLogDTO payaRequestLogDTO)
        {
            throw new NotImplementedException();
        }

        Task<string> IPayaTransferRepository.InsertPayaResponseLog(PayaResponseLogDTO payaResponseLogDTO)
        {
            throw new NotImplementedException();
        }
    }
}
