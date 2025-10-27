namespace SostavSD.Entities
{
    public class Coefficient
    {
        public int BuildingViewId {  get; set; }
        public BuildingView BuildingView { get; set; }

        public int BuildingZoneId { get; set; }
        public BuildingZone BuildingZone { get; set; }
        public string CoefficientName { get; set; }
        public string Qualifier { get; set; }
        public double? OHROPR1 { get; set; }
        public double? PlannedProfit { get; set; }  
        public double? OHROPR2 { get; set; }
        public bool Relevance {  get; set; }    
    }
}
