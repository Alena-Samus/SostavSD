namespace SostavSD.Entities
{
	public class SourceOfFinacing
	{
		public int SourceId { get; set; }
		public string SourceName { get; set; }

		public ICollection<Contract> Contracts { get; set; }
	}
}
