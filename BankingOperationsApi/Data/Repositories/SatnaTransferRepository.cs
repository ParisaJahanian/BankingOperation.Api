using Azure.Core;
using BankingOperationsApi.Data.Entities;
using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using BankingOperationsApi.Models;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace BankingOperationsApi.Data.Repositories
{
    public class SatnaTransferRepository : ISatnaTransferRepository
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
    }
}
