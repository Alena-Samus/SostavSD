namespace SostavSD.Entities
{
    public class Deppart
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupANU { get; set; }

        public ICollection<Drawing> Drawings { get; set; }
    }
}
