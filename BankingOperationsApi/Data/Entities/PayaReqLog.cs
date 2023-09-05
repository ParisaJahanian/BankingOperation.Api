namespace BankingOperationsApi.Data.Entities
{
    public class PayaReqLog : BaseEntity<String>
    {
        public PayaReqLog()
        {
            LogDateTime = DateTime.Now;
        }
        public DateTime LogDateTime { get; set; }
        public string JsonReq { get; set; }
        //***************//
        public string UserId { get; set; }
        public string PublicAppId { get; set; }
        public string ServiceId { get; set; }
        public string PublicReqId { get; set; }

        public ICollection<PayaResLog> PayaResLogs { get; set; }
    }
}
