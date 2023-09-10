using Azure.Core;
using BankingOperationsApi.Data.Entities;
using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using BankingOperationsApi.Models;
using Microsoft.OpenApi.Extensions;
using Oracle.ManagedDataAccess.Client;

namespace BankingOperationsApi.Data.Repositories
{
    public class PayaTransferRepository : IPayaTransferRepository
    {
        public IConfiguration _configuration { get; }
        private readonly ILogger<PayaTransferRepository> _logger;
        private readonly FaraboomDbContext _dbContext;

        public PayaTransferRepository(IConfiguration configuration, ILogger<PayaTransferRepository> logger,
            FaraboomDbContext dbContext)
        {
            _configuration = configuration;
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task<string> InsertPayaRequestLog(PayaRequestLogDTO payaRequestLogDTO)
        {
            string requestId = Guid.NewGuid().ToString("N");
            PayaReqLog payaReqLog = new PayaReqLog
            {
                Id = requestId,
                LogDateTime = DateTime.Now,
                JsonReq = payaRequestLogDTO.jsonRequest,
                UserId = payaRequestLogDTO.userId,
                PublicAppId = payaRequestLogDTO.publicAppId,
                ServiceId = payaRequestLogDTO.serviceId,
                PublicReqId = payaRequestLogDTO.publicRequestId
            };
            _dbContext.PayaReqLogs.Add(payaReqLog);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                return requestId;
            }
            catch (OracleException ex)
            {
                _logger.LogError( $"Exception occurred while {nameof(InsertPayaRequestLog)} -> " +
                    $"{ErrorCode.InternalDBConnectionError.GetDisplayName()}");
                throw new RamzNegarException(ErrorCode.InternalDBConnectionError, $"Exception occurred while: {nameof(InsertPayaRequestLog)}");
            }
        }

        public async Task<string> InsertPayaResponseLog(PayaResponseLogDTO payaResponseLogDTO)
        {
            string responseId = Guid.NewGuid().ToString("N");
            SatnaResLog satnaResLog = new SatnaResLog
            {
                Id = responseId,
                HTTPStatusCode = payaResponseLogDTO.satnaHttpResponseCode,
                JsonRes = payaResponseLogDTO.jsonResponse == "" ? "OK" : payaResponseLogDTO.jsonResponse,
                PublicReqId = payaResponseLogDTO.publicRequestId,
                ReqLogId = payaResponseLogDTO.satnaRequestId,
                ResCode = payaResponseLogDTO.satnaResCode
            };
            _dbContext.SatnaResLogs.Add(satnaResLog);
            try
            {
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
                return responseId;
            }
            catch (OracleException ex)
            {
                _logger.LogError(ex, $"Exception occurred while {nameof(InsertPayaResponseLog)}");
                throw new RamzNegarException(ErrorCode.InternalDBConnectionError, $"Exception occurred while: {nameof(InsertPayaResponseLog)}");
            }
        }

        public async Task<AccessTokenEntity> AddOrUpdatePayaTokenAsync(string? accessToken)
        {
            var query = _dbContext.AccessTokens.SingleOrDefault(i => i.Id == "7");
            if (query is null)
            {
                query = new AccessTokenEntity();
                query.Id = "7";
                query.TokenName = "PayaTransfer";
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
                    $"{nameof(AddOrUpdatePayaTokenAsync)} -> applyUpdateToken in AddOrUpdatePayaTokenAsync couldn't update.");
                throw new RamzNegarException(ErrorCode.SatnaTransferTokenApiError,
                    $"Exception occurred while: {nameof(AddOrUpdatePayaTokenAsync)}  => {ErrorCode.SatnaTransferTokenApiError.GetDisplayName()}");
            }

            return query;
        }
    }
}
