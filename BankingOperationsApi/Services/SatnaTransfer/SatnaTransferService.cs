using BankingOperationsApi.Models;
using AutoMapper;
using BankingOperationsApi.Data.Repositories;
using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using Microsoft.OpenApi.Extensions;
using System.Text.Json;

namespace BankingOperationsApi.Services.SatnaTransfer
{
    public class SatnaTransferService : ISatnaTransferService
    {
        public IMapper _mapper { get; }

        private readonly ISatnaTransferClient _client;
        private readonly ILogger<SatnaTransferService> _logger;
        private IConfiguration _configuration { get; set; }
        private ISatnaTransferRepository _satnaTransferRepository { get; set; }
        private IBaseRepository _baseRepository { get; set; }

        public SatnaTransferService(IMapper mapper, ILogger<SatnaTransferService> logger,
            IConfiguration configuration, ISatnaTransferClient client,
            ISatnaTransferRepository satnaTransferRepository, IBaseRepository baseRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _client = client;
            _satnaTransferRepository = satnaTransferRepository;
            _baseRepository = baseRepository;
        }
        public async Task<OutputModel> GetTokenAsync(BasePublicLogData basePublicLogData)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetTokenAsync)} request start - input is: \r\n {basePublicLogData}");
                SatnaRequestLogDTO satnaRequest = new SatnaRequestLogDTO(basePublicLogData.PublicLogData?.PublicReqId, basePublicLogData.ToString(),
                    basePublicLogData.PublicLogData?.UserId, basePublicLogData.PublicLogData?.PublicAppId, basePublicLogData.PublicLogData?.ServiceId);
                string requestId = await _satnaTransferRepository.InsertSatnaRequestLog(satnaRequest);
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
                _logger.LogError(e, $"Exception occurred while {nameof(GetTokenAsync)}");
                throw new RamzNegarException(ErrorCode.FaraboomTransferApiError,
                    $"Exception occurred while: {nameof(GetTokenAsync)} => {ErrorCode.FaraboomTransferApiError.GetDisplayName()}");
            }

        }

        public async Task<OutputModel> SatnaTransferAsync(SatnaTransferReqDTO satnaTransferReqDTO)
        {
            try
            {
                _logger.LogInformation($"{nameof(SatnaTransferAsync)} request start - input is: \r\n {satnaTransferReqDTO}");
                SatnaRequestLogDTO satnaRequest = new SatnaRequestLogDTO(satnaTransferReqDTO.PublicLogData?.PublicReqId, satnaTransferReqDTO.ToString(),
                     satnaTransferReqDTO.PublicLogData?.UserId, satnaTransferReqDTO.PublicLogData?.PublicAppId, satnaTransferReqDTO.PublicLogData?.ServiceId);
                string requestId = await _satnaTransferRepository.InsertSatnaRequestLog(satnaRequest);
                BasePublicLogData basePublic = new BasePublicLogData
                {
                    PublicLogData = satnaTransferReqDTO.PublicLogData
                };
                var satnaTransferReq = _mapper.Map<SatnaTransferReq>(satnaTransferReqDTO);
                var result = _client.GetSatnaTransferAsync(satnaTransferReq);
                return new OutputModel
                {
                    Content = result.Result.ToString(),
                    RequestId = requestId,
                    StatusCode = result.Result.StatusCode
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Exception occurred while {nameof(SatnaTransferAsync)}");
                throw new RamzNegarException(ErrorCode.FaraboomTransferApiError,
                    $"Exception occurred while: {nameof(SatnaTransferAsync)} => {ErrorCode.FaraboomTransferApiError.GetDisplayName()}");
            }
        }
    }
}
