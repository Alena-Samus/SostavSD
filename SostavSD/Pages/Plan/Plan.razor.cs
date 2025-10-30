using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Pages.ProjectSostav;

namespace SostavSD.Pages.Plan
{
    public partial class Plan
    {
        [Inject] IDrawingService DrawingService { get; set; }
        [Inject] IDeppartService DeppartService { get; set; }
        [Inject] IAuthorizedUserService AuthorizedUserService { get; set; }
        [Inject] IStringLocalizer<EditDrawingDialog> Localizer { get; set; }

        private List<DeppartModel> _groupForTable = new List<DeppartModel>();
        private List<UsersForListModel> _estimates = new List<UsersForListModel>();
        private List<DrawingModel> _drawings = new List<DrawingModel>();

        private List<string> _groups = new List<string>() { "1", "2", "3", "4","7" };

        private DeppartModel _currentDeppart = new DeppartModel();
        private UsersForListModel _currentEstimate = new UsersForListModel();
        private DrawingModel _selectedDrawing = new DrawingModel();
        private string searchString;

        string styleTableHeader = "font-size: 12px; text-align: center; padding: 0 0 0 10px; overflow-wrap: break-word; line-height: 1; position: sticky; top: 8px; z-index: 50;";
        string styleTableBody = "padding: 0; text-align: center;";

        protected override async Task OnInitializedAsync()
        {
            _groupForTable = await DeppartService.GetDeppartsByGroupsAsync(_groups);

        }

        private async Task OnGroupSelected (DeppartModel deppart)
        {
            _currentDeppart = deppart;
            _currentEstimate = new();
            _estimates.Clear();
            _estimates = AuthorizedUserService.GetListUserSostavModelByGroup(deppart.GroupANU);
            _drawings.Clear();
            _drawings = await DrawingService.GetDrawingModelsByGroupIdAsync(deppart.GroupId);
        }

        private bool FilterFuncCurrent(DrawingModel drawing) => FilterFunc(drawing, searchString);

        private bool FilterFunc(DrawingModel drawing, string searchString)
        {
            bool result = string.IsNullOrWhiteSpace(searchString)
            || ((drawing.DrawingPriority > 1) && drawing.DrawingPriority.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (!string.IsNullOrWhiteSpace(drawing.DrawingName) && drawing.DrawingName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (!string.IsNullOrWhiteSpace(drawing.DrawingReleaseDateBySchedule.ToString()) && drawing.DrawingReleaseDateBySchedule.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (!string.IsNullOrWhiteSpace(drawing.DrawingReleaseDateDepertment.ToString()) && drawing.DrawingReleaseDateDepertment.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (!string.IsNullOrWhiteSpace(drawing.DrawingDateOfAdmissionToDepartment.ToString()) && drawing.DrawingDateOfAdmissionToDepartment.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (!string.IsNullOrWhiteSpace(drawing.Status?.StatusName) && drawing.Status.StatusName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (!string.IsNullOrWhiteSpace(drawing.Group.ToString()) && drawing.Group.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (!string.IsNullOrWhiteSpace(drawing.Notes) && drawing.Notes.Contains(searchString, StringComparison.OrdinalIgnoreCase));


            return result;
        }

    }
}
