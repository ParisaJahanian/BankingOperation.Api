using BankingOperationsApi.Data.Entities;

namespace BankingOperationsApi.Data.Repositories
{
    public interface IBaseRepository
    {
        Task<string> FindAccessToken();
        Task<AccessTokenEntity> AddOrUpdateTokenAsync(string? accessToken);
    }
}
