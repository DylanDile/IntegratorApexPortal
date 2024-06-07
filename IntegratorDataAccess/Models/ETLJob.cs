namespace IntegratorDataAccess.Models
{
    public class ETLJob
    {
        public Int64 id { get; set; }
        public string LEAD_CO_CODE { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string ReturnPack { get; set; }
        public string ReturnName { get; set; }
        public long InstitutionID { get; set; }
        public int ServiceNumber { get; set; }
        public string DateEntered { get; set; }
        public string Status { get; set; }
        public string Week_Date { get; set; }
    }
}
