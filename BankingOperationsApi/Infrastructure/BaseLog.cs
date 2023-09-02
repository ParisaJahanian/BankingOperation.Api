
namespace BankingOperationsApi.Infrastructure
{
    public class BaseLog
    {
        //public ICarElectronicTollsRepository _electronicTollsRepository { get; }
        //public BaseLog(ICarElectronicTollsRepository electronicTollsRepository)
        //{
        //    _electronicTollsRepository = electronicTollsRepository;
        //}
        //public T ApiResponseSuccessByCodeProvider<T>(string response, string statusCode, string RequestId, string publicReqId) where T : new()
        //{
        //    _electronicTollsRepository.InsertCarTollsResponseLog(new CarTollsResponseLogDTO(publicReqId, Convert.ToString(response), statusCode, RequestId, statusCode));
        //    var responseResult = JsonConvert.DeserializeObject<T>(response);
        //    return responseResult;
        //}
        //public ErrorResult ApiResponeFailByCodeProvider<T>(string response, string statusCode, string RequestId, string publicReqId) where T : new()
        //{
        //    var codeProvider = new ErrorCodesProvider();
        //    codeProvider = codeProvider.errorCodesResponseResult(statusCode.ToString());
        //    _electronicTollsRepository.InsertCarTollsResponseLog(new CarTollsResponseLogDTO
        //        (publicReqId, Convert.ToString(response), codeProvider?.OutReponseCode.ToString(),
        //        RequestId, codeProvider?.SafeReponseCode.ToString()));
        //    return ServiceHelperExtension.GenerateApiErrorResponse<ErrorResult>(codeProvider);
        //}


    }
}
