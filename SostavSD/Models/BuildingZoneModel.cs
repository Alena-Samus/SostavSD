namespace SostavSD.Models
{
	public class BuildingZoneModel
	{
		public int BuildingZoneId { get; set; }
		public string BuildingZoneName { get; set; }

		public ICollection<ContractModel> Contracts { get; set; }
	}
}
