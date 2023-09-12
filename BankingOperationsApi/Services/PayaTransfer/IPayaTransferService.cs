using BankingOperationsApi.Models;

namespace BankingOperationsApi.Services.PayaTransfer
{
    public interface IPayaTransferService
    {
        Task<OutputModel> GetTokenAsync(BasePublicLogData basePublicLogData);
        Task<OutputModel> PayaTransferAsync(PayaTransferReqDTO payaTransferReqDTO);
        Task<OutputModel> PayaBatchTransferAsync(PayaBatchTransferReqDTO payaTransferReqDTO);
        Task<OutputModel> PayaTransferCancellationAsync(PayaTransferCancellationReqDTO payaTransferReqDTO);
    }
}
