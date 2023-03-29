using Microsoft.Extensions.Localization;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Pages.Contracts;
using System.Security.Claims;

namespace SostavSD.Pages.CurrentUser
{
	public partial class CurrentUserContracts
	{
		private IContractService _contractService;
		private IAuthorizedUserService _authorizedUserService;
        private IStringLocalizer<CurrentUserContracts> _localizer;


        List<ContractModel> _userContracts = new List<ContractModel>();

		public  CurrentUserContracts(IContractService contractService, IAuthorizedUserService authorizedUserService,IStringLocalizer<CurrentUserContracts> localizer) 
		{ 
			_contractService = contractService;
			_authorizedUserService = authorizedUserService;
			_localizer = localizer;
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
