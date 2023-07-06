using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSelection
{
    partial class ProjectSelection
    {
        [Inject] public IEntityManagementService EntityManagementService { get; set; }
		[Inject] public IAuthorizedUserService AuthorizedUserService { get; set; }
		[Inject] public IStringLocalizer<ProjectSelection> Localizer { get; set; }

        List<ManagerUserModel> Calculators = new();

        private List<ProjectModel> _projects = new();

        protected override async Task OnInitializedAsync()
        {
            await GetProjectsAsync();
			Calculators = await AuthorizedUserService.GetAllUsersAsync();
		}

        private async Task <List<ProjectModel>> GetProjectsAsync()
        {
            _projects = await EntityManagementService.GetProjectsAsync();
            return _projects;
        }
    }
}
