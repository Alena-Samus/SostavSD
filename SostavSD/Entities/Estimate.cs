namespace SostavSD.Entities
{
    public class Estimate
    {
        public int EstimateId { get; set; }
        public string EstimateNumber { get; set; }
        public string EstimateCode { get;set; }
        public string EstimateName { get;set; }
        public DateTime? EstimateReleaseDate { get;set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public int? ReplacementOrAdditionType { get; set; }
        public int? ReplacementOrAdditionEsimateId { get; set; }
        public string EstimatorId { get; set; }
        public UserSostav Estimator { get; set; }
        public double? SMR { get; set; }
        public double? Equipment { get; set; }
        public double? Other { get; set; }
        public double? Total { get; set; }

        public ICollection<Drawing> Drawings { get; set; }
        public ICollection<DrawingEstimate> DrawingsEstimates { get; set; }

    }
}
