using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSostav
{
    partial class DrawingsWithoutEstimates
    {
        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] IStringLocalizer<DrawingsWithoutEstimates> Localizer { get; set; }

        
        private IDrawingService _drawingService;
        private IDialogService _dialogService;
        private HashSet<DrawingModel> selectedItems = new HashSet<DrawingModel>();

        List <DrawingModel> _drawings= new ();


        private string searchString;

        string styleTableHeader = "font-size: 12px; text-align: center; padding: 0 0 0 10px; overflow-wrap: break-word; line-height: 1;";
        string styleTableBody = "padding: 0; text-align: center;";

        public DrawingsWithoutEstimates(IDrawingService drawingService,IDialogService dialog)
        {
            _drawingService = drawingService;
            _dialogService = dialog;
        }
        protected override async Task OnInitializedAsync()
        {
           _drawings = await EntityManagementService.GetDrawingModelsAsync();
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

        private async Task OpenEditDialog(TableRowClickEventArgs<DrawingModel> tableRowClickEventArgs)
        {
            //var currentProject = tableRowClickEventArgs.Item.Project;
            //if (await EntityManagementService.EditProjectAsync(currentProject))
            //{
            //    Snackbar.Add(_localizer["projectEdited"], Severity.Success);
            //    await GetProjects();
            //}
            //else
            //{
            //    Snackbar.Add(_localizer["projectNotEdited"], Severity.Error);
            //}

        }
    }
}
