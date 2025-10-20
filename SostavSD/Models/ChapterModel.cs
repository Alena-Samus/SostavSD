using SostavSD.Entities;

namespace SostavSD.Models
{
    public class ChapterModel
    {
        public int ChapterId { get; set; }
        public string ChapterName { get; set; }

        public ICollection<SubsectionModel> Sections { get; set; }
    }
}
