using BankingOperationsApi.Data.Repositories;
using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using BankingOperationsApi.Infrastructure.Extension;
using BankingOperationsApi.Models;
using Microsoft.OpenApi.Extensions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace BankingOperationsApi.Infrastructure
{

    public class BaseLog:BaseRepository
    {
        public ISatnaTransferRepository _satnaTransferRepository { get; }
        public IPayaTransferRepository _payaTransferRepository { get; }
        private ILogger<BaseLog> _logger { get; }


        public BaseLog(ISatnaTransferRepository satnaTransferRepository,
            IPayaTransferRepository payaTransferRepository,
            ILogger<BaseLog> logger)
        {
            _satnaTransferRepository = satnaTransferRepository;
            _payaTransferRepository = payaTransferRepository;
            _logger = logger;
        }
        public T ApiResponseSuccessByCodeProvider<T>(string response, string statusCode, string RequestId, string publicReqId) where T : new()
        {
            _satnaTransferRepository.InsertSatnaResponseLog(new SatnaResponseLogDTO(publicReqId, Convert.ToString(response), statusCode, RequestId, statusCode));
           
            var responseResult = JsonSerializer.Deserialize<T>(response);
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
        public  async Task<TResponse> TransferSendAsync<TRequest, TResponse>(string uriString, HttpMethod method, TRequest request,
        [CallerMemberName] string callerMethodName = null) where TResponse : ErrorResult, new() where TRequest : class
        {
            {
                var delay = TimeSpan.FromSeconds(20);
                var cancellationToken = new CancellationTokenSource(delay).Token;
                var requestHttpMessage = new HttpRequestMessage(method, uriString);
                var token = await FindAccessToken().ConfigureAwait(false);
                if (token is null)
                {
                    _logger.LogError($"token is null in the FindAccessToken method ->{ErrorCode.NotFound.GetDisplayName()}");
                    throw new RamzNegarException(ErrorCode.SatnaTokenApiError,
                                  ErrorCode.SatnaTransferApiError.GetDisplayName());
                }
                requestHttpMessage.AddFaraboomCommonHeader(_faraboomOptions, token);

                if (method == HttpMethod.Post && request != null)
                {
                    requestHttpMessage.Content =
                        new StringContent(
                            JsonSerializer.Serialize(request, ServiceHelperExtension.JsonSerializerOptions),
                    Encoding.UTF8, "application/json");
                }

                HttpResponseMessage httpResponseMessage;
                try
                {
                    httpResponseMessage = await _httpClient.SendAsync(requestHttpMessage, cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (TaskCanceledException e)
                {
                    throw new RamzNegarException(ErrorCode.SatnaTransferApiError,
                        ErrorCode.SatnaTransferApiError.GetDisplayName());
                }
                catch (Exception e)
                {
                    _logger.LogError(e,
                        $"{callerMethodName} - request: '{request}' \r\n error message: {e.Message} ");
                    throw new RamzNegarException(ErrorCode.SatnaTransferApiError,
                                  ErrorCode.SatnaTransferApiError.GetDisplayName());
                }

                var responseContent = await (httpResponseMessage?.Content?.ReadAsStringAsync())
                    .ConfigureAwait(false);

                if (!httpResponseMessage.IsSuccessStatusCode)
                    return new TResponse()
                    {
                        IsSuccess = false,
                        ResultMessage = responseContent,
                        StatusCode = httpResponseMessage.StatusCode.ToString(),
                    };

                try
                {
                    var response = JsonSerializer.Deserialize<TResponse>(responseContent,
                        ServiceHelperExtension.JsonSerializerOptions);
                    response ??= new TResponse() { IsSuccess = true, StatusCode = httpResponseMessage.StatusCode.ToString() };
                    response.IsSuccess = true;
                    response.ResultMessage = responseContent;
                    response.StatusCode = httpResponseMessage.StatusCode.ToString();
                    return response;
                }
                catch (JsonException e)
                {
                    _logger.LogError(e,
                        $"{callerMethodName} - could not serialized: '{responseContent}' to: '{typeof(TResponse)}'");
                    throw new RamzNegarException(ErrorCode.InternalError, e.Message);
                }
                catch (Exception e)
                {
                    _logger.LogError(e,
                        $"{callerMethodName} - responseContent: '{responseContent}' \r\n error message: {e.Message} ");
                    throw new RamzNegarException(ErrorCode.InternalError, e.Message);
                }
            }
        }


    }
}
