using SostavSD.Entities;

namespace SostavSD.Models
{
    public class DrawingEstimateModel
    {
        public int DrawingId { get; set; }
        public DrawingModel Drawing { get; set; }
        public int EstimateId { get; set; }
        public EstimateModel Estimate { get; set; }
    }
}
