using BankingOperationsApi.Models;

namespace BankingOperationsApi.Services.PayaTransfer
{
    public interface IPayaTransferClient
    {
        Task<TokenRes> GetTokenAsync();
        Task<PayaTransferRes> GetPayaTransferAsync(PayaTransferReq payaTransferReq);
    }
}
