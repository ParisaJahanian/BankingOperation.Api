namespace BankingOperationsApi.Data.Entities
{
    public class SatnaResLog : BaseEntity<string>
    {
        public string ResCode { get; set; }
        public string HTTPStatusCode { get; set; }
        public string JsonRes { get; set; }
        public string PublicReqId { get; set; }
        //***********//
        public string ReqLogId { get; set; }
        public SatnaReqLog SatnaReqLog { get; set; }
    }
}
