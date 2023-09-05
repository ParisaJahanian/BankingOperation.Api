using BankingOperationsApi.Models;

namespace BankingOperationsApi.Services.SatnaTransfer
{
    public interface ISatnaTransferClient
    {
        Task<TokenRes> GetTokenAsync();
    }
}
