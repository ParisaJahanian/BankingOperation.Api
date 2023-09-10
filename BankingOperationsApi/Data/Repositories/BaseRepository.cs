using Microsoft.EntityFrameworkCore;

namespace BankingOperationsApi.Data.Repositories
{
    public class BaseRepository
    {
        private FaraboomDbContext _dbContext { get; set; }
        public async Task<string> FindAccessToken()
        {
            var query = await _dbContext.AccessTokens.
                SingleOrDefaultAsync(i => i.Id == "6")
                .ConfigureAwait(false);
            return query?.AccessToken ?? string.Empty;
        }
    }
}
