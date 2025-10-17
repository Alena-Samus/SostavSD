using SostavSD.Areas.Identity.Constants;
using SostavSD.Entities;

namespace SostavSD.Models
{
    public class DrawingModel
    {
        public int DrawingId { get; set; }
        public string DrawingName { get; set; }
        public int ProjectId { get; set; }

        public ProjectModel Project { get; set; }
        public int? StatusId { get; set; }
        public StatusModel Status { get; set; }
        public int? DrawingPriority { get; set; }
        public DateTime? DrawingReleaseDateBySchedule { get; set; }
        public DateTime? DrawingReleaseDateDepertment { get; set; }
        public DateTime? DrawingDateOfAdmissionToDepartment { get; set; }
        public int? GroupId { get; set; }
        public DeppartModel Group { get; set; }
        public string Notes { get; set; }
        public string TaskFile { get; set; }

        public ICollection<EstimateModel> Estimates { get; set; }
        public ICollection<DrawingEstimateModel> DrawingsEstimates { get; set; }


    }
}
