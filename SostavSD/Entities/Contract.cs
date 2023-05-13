
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
        public DateTime? ContractDate { get; set; }
        public string City { get; set; }
        public int? CompanyID { get; set; }
        public Company Company { get; set; }

        public string? UserID { get; set; }
        public UserSostav Executor { get; set; }
        public string? CalculatorId { get; set; }
        public int? BuildingZoneId { get; set; }
        public BuildingZone BuildingZone { get; set; }

        public int? SourceOfFinancingId { get; set; }
        public SourceOfFinacing SourceOfFinacing { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
