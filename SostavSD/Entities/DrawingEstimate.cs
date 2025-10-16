namespace SostavSD.Entities
{
    public class DrawingEstimate
    {
        public int DrawingId { get; set; }
        public Drawing Drawing { get; set; }
        public int EstimateId { get; set; }
        public Estimate Estimate { get; set; }

    }
}
