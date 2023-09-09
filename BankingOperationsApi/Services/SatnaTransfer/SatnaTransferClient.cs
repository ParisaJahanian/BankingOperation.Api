using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using BankingOperationsApi.Infrastructure.Extension;
using BankingOperationsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace BankingOperationsApi.Services.SatnaTransfer
{
    public class SatnaTransferClient : ISatnaTransferClient
    {
        private readonly ILogger<SatnaTransferClient> _logger;
        private readonly HttpClient _httpClient;
        private readonly FaraboomOptions _faraboomOptions;
        public SatnaTransferClient(HttpClient httpClient, ILogger<SatnaTransferClient> logger,
            IOptions<FaraboomOptions> faraboomOptions)
        {
            _httpClient = httpClient;
            _logger = logger;
            _faraboomOptions = faraboomOptions?.Value;
        }
        public async Task<TokenRes> GetTokenAsync()
        {
            try
            {
                var loginUri = new Uri(_faraboomOptions.TokenUrl, UriKind.RelativeOrAbsolute);
                var request = new HttpRequestMessage(HttpMethod.Post, loginUri);
                request.AddFaraboomTokenHeader(_faraboomOptions);
                request.Content = ServiceHelperExtension.LoginFormUrlEncodedContent(_faraboomOptions);
                _logger.LogInformation($"{nameof(GetTokenAsync)} - request is: \r\n {JsonSerializer.Serialize(request)}");
                var response = await _httpClient.SendAsync(request)
                    .ConfigureAwait(false);
                var responseBodyJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var satnaLoginOutput =
                    JsonSerializer.Deserialize<TokenOutput>(responseBodyJson,
                        ServiceHelperExtension.JsonSerializerOptions);


                if (string.IsNullOrWhiteSpace(satnaLoginOutput?.AccessToken))
                {
                    //throw new FaraboomException(
                    //    $"{nameof(GetTokenAsync)} =>with input:{JsonSerializer.Serialize(request)}=> {response.StatusCode}/{responseBodyJson}");
                }
                return new TokenRes()
                {
                    AccessToken = satnaLoginOutput.AccessToken ?? "",
                    ExpireTime = satnaLoginOutput.ExpireTime,
                    //ExpirationDateTime =
                    //    (DateTime.UtcNow.AddSeconds(paymanLosatnaLoLoginOutputginOutput.ExpireTime))
                    IsSuccess = response.IsSuccessStatusCode,
                    StatusCode = response.StatusCode.ToString(),
                    RefreshToken = satnaLoginOutput.RefreshToken ?? ""
                };


            }
            catch (Exception e)
            {
                _logger.LogError($"Unable to PaymanLogin appropriateResponse: {nameof(GetTokenAsync)}, cause of {e.Message}");
                throw new RamzNegarException(ErrorCode.SatnaTransferApiError,
                    $"Exception occurred while: {nameof(GetTokenAsync)} => {ErrorCode.SatnaTransferApiError.GetDisplayName()}");
            }

        }

        public async Task<SatnaTransferRes> GetSatnaTransferAsync(SatnaTransferReq satnaTransferReq)
        {
            //try
            //{
            //    var loginUri = new Uri(_faraboomOptions.SatnaTransferUrl, UriKind.RelativeOrAbsolute);
            //    var request = new HttpRequestMessage(HttpMethod.Post, loginUri);
            //    var token = await GetTokenAsync();
            //    request.AddFaraboomCommonHeader(_faraboomOptions, token?.AccessToken);
            //    _logger.LogInformation($"{nameof(GetSatnaTransferAsync)} - request is: \r\n {JsonSerializer.Serialize(request)}");
            //    var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            //    var responseBodyJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            //    var satnaLoginOutput =
            //        JsonSerializer.Deserialize<SatnaTransferRes>(responseBodyJson,
            //            ServiceHelperExtension.JsonSerializerOptions);
            //    if (satnaLoginOutput is null)
            //    {
            //        _logger.LogError($"{nameof(GetSatnaTransferAsync)}");
            //        //return ServiceHelperExtension.GenerateApiErrorResponse<SatnaTransferResDTO>();
            //        return new SatnaTransferRes() { IsSuccess = false, ResultMessage = responseBodyJson , StatusCode= response.StatusCode.ToString() };
            //    }

            //    return new SatnaTransferRes()
            //    {
            //        StatusCode = response.StatusCode.ToString(),
            //        ResultMessage = responseBodyJson,
            //        IsSuccess = satnaLoginOutput.IsSuccess

            //    };
            //}
            //catch (Exception e)
            //{
            //    _logger.LogError($"Unable to GetSatnaTransferAsync appropriateResponse: {nameof(GetSatnaTransferAsync)}, cause of {e.Message}");
            //    throw new RamzNegarException(ErrorCode.SatnaTransferApiError,
            //        $"Exception occurred while: {nameof(GetSatnaTransferAsync)} => {ErrorCode.SatnaTransferApiError.GetDisplayName()}");
            //}

            var response= await SatnaTransferSendAsync<SatnaTransferReq, SatnaTransferRes>
                (_faraboomOptions.SatnaTransferUrl,HttpMethod.Post, satnaTransferReq);
            return response;
        }

        #region private
        private async Task<TResponse> SatnaTransferSendAsync<TRequest, TResponse>(string uriString, HttpMethod method, TRequest request,
             [CallerMemberName] string callerMethodName = null) where TResponse : ErrorResult, new() where TRequest : class
        {
            {
                var delay = TimeSpan.FromSeconds(20);
                var cancellationToken = new CancellationTokenSource(delay).Token;
                var requestHttpMessage = new HttpRequestMessage(method, uriString);
                var token = GetTokenAsync();
                requestHttpMessage.AddFaraboomCommonHeader(_faraboomOptions,token.Result.AccessToken);

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
                    throw new RamzNegarException(ErrorCode.SatnaTransferApiError, e.Message);
                }
                catch (Exception e)
                {
                    _logger.LogError(e,
                        $"{callerMethodName} - request: '{request}' \r\n error message: {e.Message} ");
                    throw new RamzNegarException(ErrorCode.SatnaTransferApiError, e.Message);
                }

                var responseContent = await (httpResponseMessage?.Content?.ReadAsStringAsync())
                    .ConfigureAwait(false);

                if (!httpResponseMessage.IsSuccessStatusCode)
                    return new TResponse()
                    {
                        IsSuccess = false,
                        ResultMessage = responseContent,
                    };

                try
                {
                    var response = JsonSerializer.Deserialize<TResponse>(responseContent,
                        ServiceHelperExtension.JsonSerializerOptions);
                    response ??= new TResponse() { IsSuccess = true };
                    response.IsSuccess = true;
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

        //private async Task<FaraboomTokenEntity> AddOrUpdateTokenAsync(FaraboomTokenResponse faraboomTokenResponse,
        //       FaraboomTokenEntity faraboomTokenEntity)
        //{
        //    if (faraboomTokenEntity == null)
        //    {
        //        faraboomTokenEntity = new FaraboomTokenEntity();
        //        _dbContext.FaraboomTokens.Add(faraboomTokenEntity);
        //    }
        //    faraboomTokenEntity.Token = faraboomTokenResponse.Token;
        //    faraboomTokenEntity.CreationDateTime = DateTime.UtcNow;
        //    faraboomTokenEntity.ExpirationInSecond = faraboomTokenResponse.ExpirationInSecond;
        //    faraboomTokenEntity.ExpirationDateTime = faraboomTokenResponse.ExpirationDateTime;
        //    await _dbContext.SaveChangesAsync();
        //    return faraboomTokenEntity;
        //}
        #endregion
    }
}
