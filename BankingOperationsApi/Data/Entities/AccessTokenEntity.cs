namespace BankingOperationsApi.Data.Entities
{
    public sealed class AccessTokenEntity
    {
        public string Id { get; set; }
        public DateTime TokenDateTime { get; set; }
        public string AccessToken { get; set; }
        public string TokenName { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
