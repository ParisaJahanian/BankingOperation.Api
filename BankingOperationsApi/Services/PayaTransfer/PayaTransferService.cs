using AutoMapper;
using BankingOperationsApi.Data.Repositories;
using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using BankingOperationsApi.Infrastructure.Extension;
using BankingOperationsApi.Models;
using BankingOperationsApi.Services.SatnaTransfer;
using Microsoft.OpenApi.Extensions;
using System.Text.Json;

namespace BankingOperationsApi.Services.PayaTransfer
{
    public class PayaTransferService : IPayaTransferService
    {
        public IMapper _mapper { get; }

        private readonly IPayaTransferClient _client;

        private readonly ILogger<PayaTransferService> _logger;
        private IConfiguration _configuration { get; set; }
        private IPayaTransferRepository _payaTransferRepository { get; set; }
        private IBaseRepository _baseRepository { get; set; }

        public PayaTransferService(IPayaTransferRepository payaTransferRepository,
            IConfiguration configuration, ILogger<PayaTransferService> logger,
            IPayaTransferClient client,
            IMapper mapper, IBaseRepository baseRepository)
        {
            _payaTransferRepository = payaTransferRepository;
            _configuration = configuration;
            _logger = logger;
            _client = client;
            _mapper = mapper;
            _baseRepository = baseRepository;
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
                    _ = await _baseRepository.AddOrUpdateTokenAsync(tokenResult?.AccessToken);
                }
                var tokenOutput = _mapper.Map<TokenOutput>(tokenResult);
                return new OutputModel
                {
                    Content = JsonSerializer.Serialize(tokenOutput),
                    RequestId = requestId,
                    StatusCode = tokenResult?.StatusCode,
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, $"Exception occurred while {nameof(GetTokenAsync)}");
                throw new RamzNegarException(ErrorCode.FaraboomTransferApiError,
                    $"Exception occurred while: {nameof(GetTokenAsync)} => {ErrorCode.FaraboomTransferApiError.GetDisplayName()}");
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
                var result =await _client.GetPayaTransferAsync(payaTransferReq);
                return new OutputModel
                {
                    Content = result.ResultMessage.ToString(),
                    RequestId = requestId,
                    StatusCode = result.StatusCode
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, $"Exception occurred while {nameof(PayaTransferAsync)}");
                throw new RamzNegarException(ErrorCode.FaraboomTransferApiError,
                    $"Exception occurred while: {nameof(PayaTransferAsync)} => {ErrorCode.FaraboomTransferApiError.GetDisplayName()}");
            }
        }

        public async Task<OutputModel> PayaBatchTransferAsync(PayaBatchTransferReqDTO payaTransferReqDTO)
        {
            try
            {
                _logger.LogInformation($"{nameof(PayaBatchTransferAsync)} request start - input is: \r\n {payaTransferReqDTO}");
                PayaRequestLogDTO payaRequest = new PayaRequestLogDTO(payaTransferReqDTO.PublicLogData?.PublicReqId, payaTransferReqDTO.ToString(),
                     payaTransferReqDTO.PublicLogData?.UserId, payaTransferReqDTO.PublicLogData?.PublicAppId, payaTransferReqDTO.PublicLogData?.ServiceId);
                string requestId = await _payaTransferRepository.InsertPayaRequestLog(payaRequest);
                var payaTransferReq = _mapper.Map<PayaBatchTransferReq>(payaTransferReqDTO);
                var result =await _client.GetPayaBatchTransferAsync(payaTransferReq);
                return new OutputModel
                {
                    Content = result.ResultMessage,
                    RequestId = requestId,
                    StatusCode = result.StatusCode
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, $"Exception occurred while {nameof(PayaBatchTransferAsync)}");
                throw new RamzNegarException(ErrorCode.FaraboomTransferApiError,
                    $"Exception occurred while: {nameof(PayaBatchTransferAsync)} => {ErrorCode.FaraboomTransferApiError.GetDisplayName()}");
            }
        }

        public async Task<OutputModel> PayaTransferCancellationAsync(PayaTransferCancellationReqDTO payaTransferReqDTO)
        {
            try
            {
                _logger.LogInformation($"{nameof(PayaTransferCancellationAsync)} request start - input is: \r\n {payaTransferReqDTO}");
                PayaRequestLogDTO payaRequest = new PayaRequestLogDTO(payaTransferReqDTO.PublicLogData?.PublicReqId, payaTransferReqDTO.ToString(),
                     payaTransferReqDTO.PublicLogData?.UserId, payaTransferReqDTO.PublicLogData?.PublicAppId, payaTransferReqDTO.PublicLogData?.ServiceId);
                string requestId = await _payaTransferRepository.InsertPayaRequestLog(payaRequest);
                var payaTransferReq = _mapper.Map<PayaTransferCancellationReq>(payaTransferReqDTO);
                var result =await _client.GetPayaTransferCancellationAsync(payaTransferReq);
                return new OutputModel
                {
                    Content = result.ResultMessage.ToString(),
                    RequestId = requestId,
                    StatusCode = result.StatusCode
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, $"Exception occurred while {nameof(PayaTransferCancellationAsync)}");
                throw new RamzNegarException(ErrorCode.FaraboomTransferApiError,
                    $"Exception occurred while: {nameof(PayaTransferCancellationAsync)} => {ErrorCode.FaraboomTransferApiError.GetDisplayName()}");
            }
        }
    }
}
