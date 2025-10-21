using SostavSD.Models;

namespace SostavSD.Classes.ProjectChapter
{
    public class ProjectChapter
    {
        public ChapterModel Chapter { get; set; }
        public List<SubsectionModel> Subsections = new List<SubsectionModel>();
    }
}
