using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSostav
{
    partial class ProjectInfo
    {
        [Parameter] public int ProjectId { get; set; }
        [Inject] IEntityManagementService entityManagementService { get; set; }
        [Inject] IStringLocalizer<ProjectInfo> Localizer { get; set; }
        private ProjectModel _projectModel = new();

        protected override async Task OnInitializedAsync()
        {
            _projectModel = await entityManagementService.GetProjectByIdAsync(ProjectId);
            
        }

    }
}
