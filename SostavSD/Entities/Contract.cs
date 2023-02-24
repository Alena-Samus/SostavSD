
using SostavSD.Models;
using System.ComponentModel.DataAnnotations;

namespace SostavSD.Entities
{
    public class Contract
    {
     
        public int ContractID { get; set; }
        public string ProjectName { get; set; }
        public string Index { get; set; }
        public string Order { get; set; }
        public string ContractNumber { get; set; }
        public dateFormat? ContractDate { get; set; }
        public dateFormat? ContractDateEndOfWork { get; set; }
        public string City { get; set; }
        public int? CompanyID { get; set; }
        public Company Company { get; set; }
    }
}
