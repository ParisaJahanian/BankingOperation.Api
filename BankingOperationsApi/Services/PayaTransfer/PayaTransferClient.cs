using BankingOperationsApi.Data.Repositories;
using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using BankingOperationsApi.Infrastructure.Extension;
using BankingOperationsApi.Models;
using BankingOperationsApi.Services.SatnaTransfer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using System.Text.Json;

namespace BankingOperationsApi.Services.PayaTransfer
{
    public class PayaTransferClient : IPayaTransferClient
    {
        private readonly ILogger<PayaTransferClient> _logger;
        private readonly HttpClient _httpClient;
        private readonly FaraboomOptions _faraboomOptions;
        private readonly IPayaTransferRepository _repository;
        public PayaTransferClient(ILogger<PayaTransferClient> logger, HttpClient httpClient,
            IOptions<FaraboomOptions> faraboomOptions, IPayaTransferRepository repository)
        {
            _logger = logger;
            _httpClient = httpClient;
            _faraboomOptions = faraboomOptions?.Value;
            _repository = repository;
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
                var payaLoginOutput =
                    JsonSerializer.Deserialize<TokenRes>(responseBodyJson,
                        ServiceHelperExtension.JsonSerializerOptions);
                if (string.IsNullOrWhiteSpace(payaLoginOutput?.AccessToken))
                {
                    _logger.LogError($"In the {nameof(GetTokenAsync)} access token is null-> {responseBodyJson}");
                    return ServiceHelperExtension.GenerateErrorMethodResponse<TokenRes>(ErrorCode.NotFound);
                }
                return new TokenRes()
                {
                    AccessToken = payaLoginOutput.AccessToken ?? "",
                    ExpireTime = payaLoginOutput.ExpireTime,
                    IsSuccess = response.IsSuccessStatusCode,
                    StatusCode = response.StatusCode.ToString(),
                    RefreshToken = payaLoginOutput.RefreshToken ?? "",
                    ResultMessage = responseBodyJson

                };
            }
            catch (Exception e)
            {
                _logger.LogError($"Unable to get  appropriateResponse: {nameof(GetTokenAsync)}, cause of {e.Message}");
                throw new RamzNegarException(ErrorCode.SatnaTransferApiError,
                    $"Exception occurred while: {nameof(GetTokenAsync)} => {ErrorCode.PayaTransferApiError.GetDisplayName()}");
            }
        }

        public async Task<PayaTransferRes> GetPayaTransferAsync(PayaTransferReq payaTransferReq)
        {
            var response = await SatnaTransferSendAsync<SatnaTransferReq, SatnaTransferRes>
               (_faraboomOptions.SatnaTransferUrl, HttpMethod.Post, satnaTransferReq);
            return response;
        }


    }
}
