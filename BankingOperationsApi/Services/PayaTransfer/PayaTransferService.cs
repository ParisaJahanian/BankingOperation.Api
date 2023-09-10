using AutoMapper;
using BankingOperationsApi.Data.Repositories;
using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using BankingOperationsApi.Models;
using BankingOperationsApi.Services.SatnaTransfer;
using Microsoft.OpenApi.Extensions;

namespace BankingOperationsApi.Services.PayaTransfer
{
    public class PayaTransferService : IPayaTransferService
    {
        public IMapper _mapper { get; }

        private readonly IPayaTransferClient _client;

        private readonly ILogger<PayaTransferService> _logger;
        private IConfiguration _configuration { get; set; }
        private IPayaTransferRepository _payaTransferRepository { get; set; }

        public PayaTransferService(IPayaTransferRepository payaTransferRepository,
            IConfiguration configuration, ILogger<PayaTransferService> logger, IPayaTransferClient client)
        {
            _payaTransferRepository = payaTransferRepository;
            _configuration = configuration;
            _logger = logger;
            _client = client;
        }
        public async Task<OutputModel> GetTokenAsync(BasePublicLogData basePublicLogData)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetTokenAsync)} request start - input is: \r\n {basePublicLogData}");
                PayaRequestLogDTO payaRequest = new PayaRequestLogDTO(basePublicLogData.PublicLogData?.PublicReqId, basePublicLogData.ToString(),
                    basePublicLogData.PublicLogData?.UserId, basePublicLogData.PublicLogData?.PublicAppId, basePublicLogData.PublicLogData?.ServiceId);
                string requestId = await _payaTransferRepository.InsertPayaRequestLog(payaRequest);
                var tokenResult = await _client.GetTokenAsync();
                if (tokenResult != null && tokenResult.IsSuccess)
                {
                    _ = await _payaTransferRepository.AddOrUpdatePayaTokenAsync(tokenResult?.AccessToken);
                }
                //var test = JsonSerializer.Deserialize<TokenOutput>(tokenResult?.ResultMessage,
                //          ServiceHelperExtension.JsonSerializerOptions);
                //  var mapped = _mapper.Map<TokenRes,TokenOutput>(tokenResult);
                return new OutputModel
                {
                    Content = tokenResult?.ResultMessage,
                    RequestId = requestId,
                    StatusCode = tokenResult.StatusCode,
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Exception occurred while {nameof(GetTokenAsync)}");
                throw new RamzNegarException(ErrorCode.PayaTransferApiError,
                    $"Exception occurred while: {nameof(GetTokenAsync)} => {ErrorCode.PayaTransferApiError.GetDisplayName()}");
            }

        }

        public async Task<OutputModel> PayaTransferAsync(PayaTransferReqDTO payaTransferReqDTO)
        {
            try
            {
                _logger.LogInformation($"{nameof(PayaTransferAsync)} request start - input is: \r\n {payaTransferReqDTO}");
                PayaRequestLogDTO payaRequest = new PayaRequestLogDTO(payaTransferReqDTO.PublicLogData?.PublicReqId, payaTransferReqDTO.ToString(),
                     payaTransferReqDTO.PublicLogData?.UserId, payaTransferReqDTO.PublicLogData?.PublicAppId, payaTransferReqDTO.PublicLogData?.ServiceId);
                string requestId = await _payaTransferRepository.InsertPayaRequestLog(payaRequest);
                var payaTransferReq = _mapper.Map<PayaTransferReq>(payaTransferReqDTO);
                var result = _client.GetPayaTransferAsync(payaTransferReq);
                return new OutputModel
                {
                    Content = result.Result.ToString(),
                    RequestId = requestId,
                    StatusCode = result.Result.StatusCode
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Exception occurred while {nameof(PayaTransferAsync)}");
                throw new RamzNegarException(ErrorCode.PayaTransferApiError,
                    $"Exception occurred while: {nameof(PayaTransferAsync)} => {ErrorCode.PayaTransferApiError.GetDisplayName()}");
            }
        }
    }
}
