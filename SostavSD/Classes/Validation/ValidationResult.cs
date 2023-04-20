namespace SostavSD.Classes.Validation
{
	public class ValidationResult
	{
		public List<string> Errors = new List<string>();
		public bool IsValid => !Errors.Any();
	}
}
