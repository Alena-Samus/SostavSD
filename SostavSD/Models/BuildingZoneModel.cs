using SostavSD.Entities;

namespace SostavSD.Models
{
	public class BuildingZoneModel
	{
		public int BuildingZoneId { get; set; }
		public string BuildingZoneName { get; set; }

		public ICollection<ContractModel> Contracts { get; set; }

        public ICollection<BuildingViewModel> BuildingZones { get; set; }
        public ICollection<CoefficientModel> Coefficients { get; set; }
    }
}
