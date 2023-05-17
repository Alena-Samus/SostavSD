using Microsoft.Extensions.Localization;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Pages.Contracts;

namespace SostavSD.Pages.Projects
{
    partial class ProjectListTable
    {
        private IProjectForTableService _projectService;
        private IStringLocalizer<ProjectListTable> _localizer;

        private List<ProjectForTableModel> _projects;
        string searchString;
        string styleTableHeader = "font-size: 12px; text-align: center; padding: 0 0 0 10px; overflow-wrap: break-word; line-height: 1;";
		string styleTableBody = "padding: 0; text-align: center;";
    

		public ProjectListTable(IProjectForTableService projectService, IStringLocalizer<ProjectListTable> localizer)
        {
            _projectService = projectService;
            _localizer = localizer;
        }

        protected override async Task OnInitializedAsync()
        {
            _projects = await _projectService.GetProjectsAsync();
        }

        private bool FilterFuncCurrent(ProjectForTableModel project) => FilterFunc(project, searchString);
        private bool FilterFunc(ProjectForTableModel project, string searchString)
        {
            bool result = false;
            return result;
        }
    }
}
