using Azure.Core;
using BankingOperationsApi.Data.Entities;
using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using BankingOperationsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Oracle.ManagedDataAccess.Client;

namespace BankingOperationsApi.Data.Repositories
{
    public class SatnaTransferRepository : BaseRepository, ISatnaTransferRepository 
    {
        public IConfiguration _configuration { get; }
        private readonly ILogger<SatnaTransferRepository> _logger;
        private readonly FaraboomDbContext _dbContext;

        public SatnaTransferRepository(IConfiguration configuration, ILogger<SatnaTransferRepository> logger,
            FaraboomDbContext dbContext)
        {
            _configuration = configuration;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<string> InsertSatnaRequestLog(SatnaRequestLogDTO satnaRequestLogDTO)
        {
            string requestId = Guid.NewGuid().ToString("N");
            SatnaReqLog satnaReqLog = new SatnaReqLog
            {
                Id = requestId,
                LogDateTime = DateTime.Now,
                JsonReq = satnaRequestLogDTO.jsonRequest,
                UserId = satnaRequestLogDTO.userId,
                PublicAppId = satnaRequestLogDTO.publicAppId,
                ServiceId = satnaRequestLogDTO.serviceId,
                PublicReqId = satnaRequestLogDTO.publicRequestId
            };
            _dbContext.SatnaReqLogs.Add(satnaReqLog);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                return requestId;
            }
            catch (OracleException ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(InsertSatnaRequestLog)}");
                throw new RamzNegarException(ErrorCode.InternalDBConnectionError, $"Exception occurred while: {nameof(InsertSatnaRequestLog)}");
            }
        }

        public async Task<string> InsertSatnaResponseLog(SatnaResponseLogDTO satnaResponseLogDTO)
        {
            string responseId = Guid.NewGuid().ToString("N");
            SatnaResLog satnaResLog = new SatnaResLog
            {
                Id = responseId,
                HTTPStatusCode = satnaResponseLogDTO.satnaHttpResponseCode,
                JsonRes = satnaResponseLogDTO.jsonResponse == "" ? "OK" : satnaResponseLogDTO.jsonResponse,
                PublicReqId = satnaResponseLogDTO.publicRequestId,
                ReqLogId = satnaResponseLogDTO.satnaRequestId,
                ResCode = satnaResponseLogDTO.satnaResCode
            };
            _dbContext.SatnaResLogs.Add(satnaResLog);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                return responseId;
            }
            catch (OracleException ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(InsertSatnaResponseLog)}");
                throw new RamzNegarException(ErrorCode.InternalDBConnectionError, $"Exception occurred while: {nameof(InsertSatnaResponseLog)}");
            }
        }

        public async Task<AccessTokenEntity> AddOrUpdateSatnaTokenAsync(string? accessToken)
        {
            var query = _dbContext.AccessTokens.SingleOrDefault(i => i.Id == "6");
            if (query is null)
            {
                query = new AccessTokenEntity();
                query.Id = "6";
                query.TokenName = "SatnaTranfer";
                await _dbContext.AccessTokens.AddAsync(query).ConfigureAwait(false);
            }
            query.AccessToken = accessToken;
            query.TokenDateTime = DateTime.UtcNow;
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError(e,
                    $"{nameof(AddOrUpdateSatnaTokenAsync)} -> applyUpdateToken in AddOrUpdateSatnaTokenAsync couldn't update.");
                throw new RamzNegarException(ErrorCode.SatnaTransferTokenApiError,
                    $"Exception occurred while: {nameof(AddOrUpdateSatnaTokenAsync)}  => {ErrorCode.SatnaTransferTokenApiError.GetDisplayName()}");
            }

            return query;
        }
      
    }
}
