using SostavSD.Models;

namespace SostavSD.Classes.Validation
{
	public class CompanyModelValidator
	{
		public ValidationResult Validate(CompanyModel company) 
		{
			var result = new ValidationResult();
			if(string.IsNullOrWhiteSpace(company.CompanyName))
			{
				result.Errors.Add("CompanyName is empty");
			}
			if (string.IsNullOrWhiteSpace(company.CompanyDetails))
			{
				result.Errors.Add("CompanyDetails is empty");
			}

			return result;
		}
	}
}
