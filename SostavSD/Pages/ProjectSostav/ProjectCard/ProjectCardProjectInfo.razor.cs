using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSostav.ProjectCard
{
    public partial class ProjectCardProjectInfo
    {
        [Parameter] public int ProjectId { get; set; }
        [Inject] IEntityManagementService entityManagementService { get; set; }
        [Inject] public IAuthorizedUserService AuthorizedUserService { get; set; }

        [Inject] IStringLocalizer<ProjectCardProjectInfo> Localizer { get; set; }
        private ProjectModel _projectModel = new();
        string calculatorName;


        protected override async Task OnInitializedAsync()
        {
            _projectModel = await entityManagementService.GetProjectByIdAsync(ProjectId);
           ManagerUserModel _calculatorName = await AuthorizedUserService.GetSingleUser(_projectModel.Contract.CalculatorId);
            if (_calculatorName != null) 
            {
                calculatorName = _calculatorName.UserSurname;
            }
        }

    }
}
