using Microsoft.Extensions.Localization;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Pages.Contracts;

namespace SostavSD.Pages.Projects
{
    partial class ProjectListTable
    {
        private IProjectService _projectService;
        private IStringLocalizer<ProjectModel> _localizer;

        private List<ProjectModel> _projects;
        string searchString;

        public ProjectListTable(IProjectService projectService, IStringLocalizer<ProjectModel> localizer)
        {
            _projectService = projectService;
            _localizer = localizer;
        }

        protected override async Task OnInitializedAsync()
        {
            _projects = await _projectService.GetProjectsAsync();
        }

        private bool FilterFuncCurrent(ProjectModel project) => FilterFunc(project, searchString);
        private bool FilterFunc(ProjectModel project, string searchString)
        {
            bool result = false;
            return result;
        }
    }
}
