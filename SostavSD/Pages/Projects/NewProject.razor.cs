using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Runtime.CompilerServices;

namespace SostavSD.Pages.Projects
{
	partial class NewProject
	{
		private NavigationManager _navigationManager;
		
		[Inject] IStringLocalizer<NewProject> Localizer { get; set; }
		[Inject] IContractService ContractService { get; set; }

	

		private List<ContractModel> _contracts = new();

		private ContractModel _selectedContract;
		private string _toProject = "/projects";
		private string _toContracts = "/contracts";
		private string _class = "d-inline-block gap-2";

		private ProjectModel _newProject;

		protected override async Task OnInitializedAsync()
		{
			_contracts = await ContractService.GetAllContract();
			_newProject = new ProjectModel();
		}
		public NewProject(NavigationManager navigationManager)
		{
			_navigationManager= navigationManager;

		}
		private void GoToPage(string adress)
		{
			_navigationManager.NavigateTo(adress);
		}
		
		//protected async Task<IEnumerable<string>> SearchItems(string value)
		//{
		//	return new string[0];
		//}
		
	}
}
