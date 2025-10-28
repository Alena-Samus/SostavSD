using Microsoft.AspNetCore.Components;
using MimeKit.Cryptography;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSostav.ProjectCard
{
    public partial class ProjectCard
    {
        [Parameter] public int ProjectId { get; set; }

        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] ICoefficientService CoefficientService { get; set; }

        private List<CoefficientModel> coefficients = new List<CoefficientModel>();

        private ProjectModel currentProject = new ProjectModel();

        private int? buildingViewId;
        private int? buildingZoneId;

        protected override async Task OnInitializedAsync()
        {
            currentProject = await EntityManagementService.GetProjectByIdAsync(ProjectId);
            buildingViewId = currentProject.BuildingViewId;
            buildingZoneId = currentProject.Contract.BuildingZoneId;
            if (buildingViewId != null && buildingZoneId != null)
            {
                coefficients = await CoefficientService.GetCoefficiensByBuildingViewIdBuildingZoneId((int)buildingViewId, (int)buildingZoneId);

            }
        }

    }
}
