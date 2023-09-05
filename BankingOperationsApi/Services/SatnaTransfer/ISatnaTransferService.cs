using BankingOperationsApi.Models;

namespace BankingOperationsApi.Services.SatnaTransfer
{
    public interface ISatnaTransferService
    {
        Task<OutputModel> GetTokenAsync(BasePublicLogData basePublicLogData);
        Task<OutputModel> SatnaTransferAsync(SatnaTransferReqDTO satnaTransferReqDTO);
        Task<OutputModel> PayaTransferAsync(PayaTransferReqDTO payaTransferReqDTO);
        Task<OutputModel> PayaBatchTransferAsync(PayaBatchTransferReqDTO payaBatchTransferReqDTO);

    }
}
