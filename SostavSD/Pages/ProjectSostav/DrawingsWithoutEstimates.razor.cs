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
        [Inject] ISnackbar Snackbar { get; set; }

        [Parameter] public int ProjectID { get; set; }

        private NavigationManager _navigationManager;
        private IDrawingService _drawingService;
        private IDialogService _dialogService;
        private HashSet<DrawingModel> selectedItems = new HashSet<DrawingModel>();
        private SortDirection _sortDirection = SortDirection.None;

        private TableState _tableState = new();
        private MudTable<DrawingModel> tableRef;

        List <DrawingModel> _drawings= new ();

        private string _toNewDrawing = "/sostav/newdrawing";


        private string searchString;

        string styleTableHeader = "font-size: 12px; text-align: center; padding: 0 0 0 10px; overflow-wrap: break-word; line-height: 1;";
        string styleTableBody = "padding: 0; text-align: center;";

        public DrawingsWithoutEstimates(IDrawingService drawingService,IDialogService dialog, NavigationManager navigationManager)
        {
            _drawingService = drawingService;
            _dialogService = dialog;
            _navigationManager = navigationManager;
        }
        protected override async Task OnInitializedAsync()
        {
           _drawings = await EntityManagementService.GetDrawingModelByIdAsync(ProjectID);
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



        //  // Обработчик сортировки
        //private async Task OnSort(TableState state)
        //{
        //    _tableState = state;
        //    _drawings = await GetSortedData(state);
        //    tableRef.ReloadServerData();
        //}
        private Task<List<DrawingModel>> GetSortedData(TableState state)
        {
            var data = _drawings.AsQueryable();

            // Сортировка
            switch (state.SortLabel)
            {
                case nameof(DrawingModel.DrawingDateOfAdmissionToDepartment):
                    data = state.SortDirection == SortDirection.Ascending ?
                        data.OrderBy(x => x.DrawingDateOfAdmissionToDepartment) :
                        data.OrderByDescending(x => x.DrawingDateOfAdmissionToDepartment);
                    break;
                case nameof(DrawingModel.DrawingReleaseDateBySchedule):
                    data = state.SortDirection == SortDirection.Ascending ?
                        data.OrderBy(x => x.DrawingReleaseDateBySchedule) :
                        data.OrderByDescending(x => x.DrawingReleaseDateBySchedule);
                    break;
                case nameof(DrawingModel.DrawingReleaseDateDepertment):
                    data = state.SortDirection == SortDirection.Ascending ?
                        data.OrderBy(x => x.DrawingReleaseDateDepertment) :
                        data.OrderByDescending(x => x.DrawingReleaseDateDepertment);
                    break;
                case nameof(DrawingModel.DrawingName):
                    data = state.SortDirection == SortDirection.Ascending ?
                        data.OrderBy(x => x.DrawingName) :
                        data.OrderByDescending(x => x.DrawingName);
                    break;
                    // Можно добавить другие поля для сортировки по аналогии
            }

            return Task.FromResult(data.ToList());
        }

        private async Task<TableData<DrawingModel>> LoadData(TableState state)
        {
            return new TableData<DrawingModel>
            {
                TotalItems = _drawings.Count,
                Items = await GetSortedData(state)
            };
        }

        private void NavigateToPage()
        {           
			_navigationManager.NavigateTo($"/sostav/newdrawing/{ProjectID}");
		}

        private async Task RemoveDrawings()
        {
            if (selectedItems.Count != 0)
            {
                foreach (var item in selectedItems)
                {
                    await EntityManagementService.RemoveDrawingsAsync(item.DrawingId);
                }
                Snackbar.Add(Localizer["itemsRemoved"], Severity.Success);
                //await GetProjects();
            }
            else
            {
                Snackbar.Add(Localizer["noItems"], Severity.Info);
            }
        }
        private async Task CopyDrawings()
        {
            List<DrawingModel> _drawingsForCopy = new List<DrawingModel>();
            foreach (var item in selectedItems) 
            {
                DrawingModel _drawing = new DrawingModel
                {
                    DrawingName = $"{item.DrawingName} Копия",
                    ProjectId = item.ProjectId,
                    DrawingDateOfAdmissionToDepartment = item.DrawingDateOfAdmissionToDepartment,
                };
                _drawingsForCopy.Add( _drawing );
            }

            if (_drawingsForCopy.Count > 0 )
            {

                if (await EntityManagementService.AddDrawingsAsync(_drawingsForCopy))
                {
                    Snackbar.Add("Copied", Severity.Success);
                }
                else
                {
                    Snackbar.Add("Not copied", Severity.Error);
                }
               
            }
            StateHasChanged();
        }
    }
}

