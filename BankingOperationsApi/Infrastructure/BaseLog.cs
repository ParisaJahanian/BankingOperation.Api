
using BankingOperationsApi.Data.Repositories;
using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Infrastructure.Extension;
using BankingOperationsApi.Models;
using Newtonsoft.Json;

namespace BankingOperationsApi.Infrastructure
{
    public class BaseLog
    {
        public ISatnaTransferRepository _satnaTransferRepository { get; }
        public BaseLog(ISatnaTransferRepository satnaTransferRepository)
        {
            _satnaTransferRepository = satnaTransferRepository;
        }
        public T ApiResponseSuccessByCodeProvider<T>(string response, string statusCode, string RequestId, string publicReqId) where T : new()
        {
            _satnaTransferRepository.InsertSatnaResponseLog(new SatnaResponseLogDTO(publicReqId, Convert.ToString(response), statusCode, RequestId, statusCode));
            var responseResult = JsonConvert.DeserializeObject<T>(response);
            return responseResult;
        }
        public ErrorResult ApiResponeFailByCodeProvider<T>(string response, string statusCode, string RequestId, string publicReqId) where T : new()
        {
            var codeProvider = new ErrorCodesProvider();
            codeProvider = codeProvider.errorCodesResponseResult(statusCode.ToString());
            _satnaTransferRepository.InsertSatnaResponseLog(new SatnaResponseLogDTO
                (publicReqId, Convert.ToString(response), codeProvider?.OutReponseCode.ToString(),
                RequestId, codeProvider?.SafeReponseCode.ToString()));
            return ServiceHelperExtension.GenerateApiErrorResponse<ErrorResult>(codeProvider);
        }


    }
}
