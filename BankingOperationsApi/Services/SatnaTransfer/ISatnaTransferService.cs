using BankingOperationsApi.Models;

namespace BankingOperationsApi.Services.SatnaTransfer
{
    public interface ISatnaTransferService
    {
        Task<string> LoginAsync(BasePublicLogData basePublicLogData);
    }
}
