using SostavSD.Entities;
using System.ComponentModel.DataAnnotations;

namespace SostavSD.Models
{
    public class ContractModel
    {

        [Key]
		public int ContractID { get; set; }
		public string ProjectName { get; set; }
		public string Index { get; set; }
		public string Order { get; set; }
		public string ContractNumber { get; set; }
		public DateTime? ContractDate { get; set; }
		public string City { get; set; }
		public int CompanyID { get; set; }
		public CompanyModel Company { get; set; }

		public string? UserID { get; set; }
		public UserSostavModel Executor { get; set; }
		public string? CalculatorId { get; set; }
		public int? BuildingZoneId { get; set; }
		public BuildingZoneModel BuildingZone { get; set; }

		public int? SourceOfFinancingId { get; set; }
		public SourceOfFinacingModel SourceOfFinacing { get; set; }

	}
}
