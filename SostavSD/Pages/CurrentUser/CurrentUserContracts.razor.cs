using SostavSD.Interfaces;
using SostavSD.Models;
using System.Security.Claims;

namespace SostavSD.Pages.CurrentUser
{
	public partial class CurrentUserContracts
	{
		private IContractService _contractService;
		private IAuthorizedUserService _authorizedUserService;

		List<ContractModel> _userContracts = new List<ContractModel>();

		public  CurrentUserContracts(IContractService contractService, IAuthorizedUserService authorizedUserService) 
		{ 
			_contractService = contractService;
			_authorizedUserService = authorizedUserService;
		}

		protected override async Task OnInitializedAsync()
		{
			var user = await _authorizedUserService.GetCurrentUserAsync();
			string userId;
			userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value.ToString();

			_userContracts = await _contractService.GetCurrentUserContracts(userId);
		}
	}
}
