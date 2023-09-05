using Azure.Core;
using Azure;
using BankingOperationsApi.Models;
using AutoMapper;
using BankingOperationsApi.Data;
using BankingOperationsApi.Data.Repositories;
using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;

namespace BankingOperationsApi.Services.SatnaTransfer
{
    public class SatnaTransferService : ISatnaTransferService
    {
        public IMapper _mapper { get; }

        private readonly ISatnaTransferClient _client;
        private readonly ILogger<SatnaTransferService> _logger;
        private IConfiguration _configuration { get; set; }
        private ISatnaTransferRepository _satnaTransferRepository{ get; set; }
        public SatnaTransferService(IMapper mapper, ILogger<SatnaTransferService> logger,
            IConfiguration configuration, ISatnaTransferClient client, 
            ISatnaTransferRepository satnaTransferRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _client = client;
            _satnaTransferRepository = satnaTransferRepository;
        }
        public async Task<OutputModel> GetTokenAsync(BasePublicLogData basePublicLogData)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetTokenAsync)} request start - input is: \r\n {basePublicLogData} for getting token manually.");
                SatnaRequestLogDTO carTolls = new SatnaRequestLogDTO(basePublicLogData.PublicLogData?.PublicReqId, basePublicLogData.ToString(),
                    basePublicLogData.PublicLogData?.UserId, basePublicLogData.PublicLogData?.PublicAppId, basePublicLogData.PublicLogData?.ServiceId);
                string requestId = await _satnaTransferRepository.InsertSatnaRequestLog(carTolls);
                var tokenResult = await _client.GetTokenAsync();
                return new OutputModel
                {
                    Content = tokenResult.ResultMessage,
                    RequestId= requestId,
                    StatusCode= tokenResult.StatusCode,
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Exception occurred while {nameof(GetTokenAsync)}");
                throw new RamzNegarException(ErrorCode.InternalError, $"Exception occurred while: {nameof(GetTokenAsync)} => {e.Message}");
            }

        }

        public async Task<OutputModel> PayaBatchTransferAsync(PayaBatchTransferReqDTO payaBatchTransferReqDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<OutputModel> PayaTransferAsync(PayaTransferReqDTO payaTransferReqDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<OutputModel> SatnaTransferAsync(SatnaTransferReqDTO satnaTransferReqDTO)
        {
            throw new NotImplementedException();
        }
    }
}
