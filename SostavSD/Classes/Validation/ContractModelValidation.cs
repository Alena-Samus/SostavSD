﻿using SostavSD.Models;


namespace SostavSD.Classes.Validation
{
	public class ContractModelValidation
	{
		public ValidationResult Validate(ContractModel contract)
		{
			var result = new ValidationResult();
			
			if (contract.CompanyID == 0)
			{
				result.Errors.Add("Customer is empty");
			}
			if (contract.ContractDate > DateTime.Today)
			{
				result.Errors.Add("ContractDate is greater than current date");
			}

			return result;
		}
	}
}
