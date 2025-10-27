namespace SostavSD.Entities
{
	public class BuildingZone
	{
		public int BuildingZoneId { get; set; }
		public string BuildingZoneName { get; set; }

		public ICollection<Contract> Contracts { get; set; }
		public ICollection<BuildingView> BuildingViews { get; set; }
        public ICollection<Coefficient> Coefficients { get; set; }
    }
}
