namespace SostavSD.Models
{
	public class SourceOfFinacingModel
	{
		public int SourceId { get; set; }
		public string SourceName { get; set; }

		public ICollection<ContractModel> Contracts { get; set; }
	}
}
