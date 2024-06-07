namespace IntegratorDataAccess.Models
{
    public class ReturnsModel
    {
        public Int64 RETURNS_ID { get; set; }
        public Int64 ReturnNumber { get; set; }
        public string ReturnPack { get; set; }
        public string ReturnName { get; set; }
        public string ReturnType { get; set; }
        public Int64 SortOrder { get; set; }
        public string DateEntered { get; set; }
        public string LastEditedBy { get; set; }
        public string DateLastEdited { get; set; }
        public string ReturnFullName { get; set; }
        public string LEAD_CO_MNE { get; set; }
        public string LEAD_CO_CODE { get; set; }
        public string BRANCH_MNE { get; set; }
        public string BRANCH_CODE { get; set; }
        public string MIS_DATE { get; set; }
    }
}
