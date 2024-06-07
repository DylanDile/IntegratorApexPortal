using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratorDataAccess.Models
{
    public class InstitutionModel
    {
        public Int64 InstitutionID { get; set; }
        public Int64 InstitutionTypeID { get; set; }
        public string InstName { get; set; }
        public string InstRegion { get; set; }
        public string InstTelephoneNumbers { get; set; }
        public string InstType { get; set; }
        public string InstEmail { get; set; }
        public string InstOperatingStatus { get; set; }
        public string InstCode { get; set; }
        public string REFERENCE_ORASS { get; set; }
        public string LEAD_CO_CODE { get; set; }
        public string LEAD_CO_NAME { get; set; }
    }
}
