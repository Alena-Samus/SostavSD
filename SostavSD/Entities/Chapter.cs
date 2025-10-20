namespace SostavSD.Entities
{
    public class Chapter
    {
        public int ChapterId { get; set; }
        public string ChapterName { get; set; }

        public ICollection<Subsection> Sections { get; set; }
    }
}
