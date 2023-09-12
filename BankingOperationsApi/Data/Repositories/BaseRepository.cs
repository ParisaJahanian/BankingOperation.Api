using BankingOperationsApi.Data.Entities;
using BankingOperationsApi.ErrorHandling;
using BankingOperationsApi.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace BankingOperationsApi.Data.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private FaraboomDbContext _dbContext { get; set; }
        private ILogger<BaseRepository> _logger;
        public BaseRepository(FaraboomDbContext dbContext, 
            ILogger<BaseRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<string> FindAccessToken()
        {
            var query = await _dbContext.AccessTokens.
                SingleOrDefaultAsync(i => i.Id == "6")
                .ConfigureAwait(false);
            return query?.AccessToken ?? string.Empty;
        }
        public async Task<AccessTokenEntity> AddOrUpdateTokenAsync(string? accessToken)
        {
            var query = _dbContext.AccessTokens.SingleOrDefault(i => i.Id == "6");
            if (query is null)
            {
                query = new AccessTokenEntity();
                query.Id = "6";
                query.TokenName = "FaraboomTransfer";
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
                _logger.LogError(e.Message,
                    $"{nameof(AddOrUpdateTokenAsync)} -> applyUpdateToken in AddOrUpdateSatnaTokenAsync couldn't update.");
                throw new RamzNegarException(ErrorCode.FaraboomTransferTokenApiError,
                    $"Exception occurred while: {nameof(AddOrUpdateTokenAsync)}  => {ErrorCode.FaraboomTransferTokenApiError.GetDisplayName()}");
            }

            return query;
        }
    }
}
