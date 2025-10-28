using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Pages.Projects;
using SostavSD.Services;

namespace SostavSD.Pages.ProjectSostav.ProjectCard
{
    public partial class ProjectCardProjectInfo
    {
        [Parameter] public ProjectModel Project { get; set; }
        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] public IAuthorizedUserService AuthorizedUserService { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] IStringLocalizer<ProjectCardProjectInfo> Localizer { get; set; }

        private ProjectModel _projectModel = new();
        string calculatorName;


        protected override async Task OnInitializedAsync()
        {

           ManagerUserModel _calculatorName = await AuthorizedUserService.GetSingleUser(Project.Contract?.CalculatorId);
            if (_calculatorName != null) 
            {
                calculatorName = _calculatorName.UserSurname;
            }
        }
        private async Task ChangeCiC()
        {
            var parameters = new DialogParameters();

            parameters.Add("ProjectCiC", _projectModel.CiCVersion);
            var dialog = await DialogService.Show<ChangeCiCDialog>("Edit", parameters).Result;
            if (dialog.Data != null)
            {
                _projectModel.CiCVersion = (string)dialog.Data;
                if (await EntityManagementService.UpdateCiCVersionAsync(_projectModel.ProjectId, _projectModel.CiCVersion))
                {
                    Snackbar.Add(@Localizer["changed"], Severity.Success);
                }
                else
                {
                    Snackbar.Add(@Localizer["notchanged"], Severity.Error);
                }
            }
        }

    }
}
