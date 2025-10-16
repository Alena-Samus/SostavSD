using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using NLog;
using NLog.Fluent;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Services;

namespace SostavSD.Pages.ProjectSelection
{
	partial class IndividualProject
	{
		[Parameter] public ProjectModel	Project { get; set; }
		[Parameter] public List<ManagerUserModel> Calculator { get; set; }
	
		[Inject] public IAuthorizedUserService AuthorizedUserService { get; set; }
		[Inject] public NavigationManager NavigationManager { get; set; }

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        string calculatorName;



		protected override async Task OnInitializedAsync()
		{
			calculatorName = Calculator.FirstOrDefault(x => x.UserId == Project.Contract.CalculatorId)?.UserSurname.ToString();
        }
		private void Navigate()
		{
			try
			{
                NavigationManager.NavigateTo($"/sostav/{Project.ProjectId}");
            }
			catch (Exception ex) 
			{
				logger.Error(ex);
                throw;
			}

        }
	}
}
