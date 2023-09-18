using BankingOperationsApi.Data.Repositories;
using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using BankingOperationsApi.Infrastructure;
using BankingOperationsApi.Infrastructure.Extension;
using BankingOperationsApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using System.Text.Json;

namespace BankingOperationsApi.Services.SatnaTransfer
{
    public class SatnaTransferClient : ISatnaTransferClient
    {
        private readonly ILogger<SatnaTransferClient> _logger;
        private readonly HttpClient _httpClient;
        private readonly FaraboomOptions _faraboomOptions;
        private readonly ISatnaTransferRepository _repository;
        private readonly BaseLog _baseLog;
        public SatnaTransferClient(HttpClient httpClient, ILogger<SatnaTransferClient> logger,
            IOptions<FaraboomOptions> faraboomOptions, ISatnaTransferRepository repository, BaseLog baseLog)
        {
            _httpClient = httpClient;
            _logger = logger;
            _faraboomOptions = faraboomOptions?.Value;
            _repository = repository;
            _baseLog = baseLog;
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
                    JsonSerializer.Deserialize<TokenRes>(responseBodyJson,
                        ServiceHelperExtension.JsonSerializerOptions);
                if (string.IsNullOrWhiteSpace(satnaLoginOutput?.AccessToken))
                {
                    _logger.LogError($"In the {nameof(GetTokenAsync)} access token is null-> {responseBodyJson}");
                    return ServiceHelperExtension.GenerateErrorMethodResponse<TokenRes>(ErrorCode.NotFound);
                }
                return new TokenRes()
                {
                    AccessToken = satnaLoginOutput?.AccessToken ?? "",
                    ExpireTime = satnaLoginOutput.ExpireTime,
                    IsSuccess = response.IsSuccessStatusCode,
                    StatusCode = response.StatusCode.ToString(),
                    RefreshToken = satnaLoginOutput.RefreshToken ?? "",
                    ResultMessage = responseBodyJson

                };
            }
            catch (Exception e)
            {
                _logger.LogError($"Unable to get appropriateResponse: {nameof(GetTokenAsync)}, cause of {e.Message}");
                throw new RamzNegarException(ErrorCode.FaraboomTransferApiError,
                    $"Exception occurred while: {nameof(GetTokenAsync)} => {ErrorCode.FaraboomTransferApiError.GetDisplayName()}");
            }

        }

        public async Task<SatnaTransferRes> GetSatnaTransferAsync(SatnaTransferReq satnaTransferReq)
        {
            var tokenResult = await GetTokenAsync();
            var response = await _baseLog.TransferSendAsync<SatnaTransferReq, SatnaTransferRes>
                (_faraboomOptions.SatnaTransferUrl, HttpMethod.Post, satnaTransferReq,tokenResult.AccessToken);
            return response;
        }

      
    }
}
