using SostavSD.Entities;

namespace SostavSD.Models
{
    public class CoefficientModel
    {
        public int CoefficientId { get; set; }
        public int BuildingViewId { get; set; }
        public BuildingViewModel BuildingView { get; set; }

        public int BuildingZoneId { get; set; }
        public BuildingZoneModel BuildingZone { get; set; }

        public string CoefficientName { get; set; }
        public string Qualifier { get; set; }
        public double? OHROPR1 { get; set; }
        public double? PlannedProfit { get; set; }
        public double? OHROPR2 { get; set; }
        public bool Relevance { get; set; }
    }
}
