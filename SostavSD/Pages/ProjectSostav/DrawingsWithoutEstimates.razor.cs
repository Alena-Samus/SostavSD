using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Diagnostics;

namespace SostavSD.Pages.ProjectSostav
{
    partial class DrawingsWithoutEstimates
    {
        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] IStringLocalizer<DrawingsWithoutEstimates> Localizer { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] IDialogService _dialogService { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }

        [Parameter] public int ProjectID { get; set; }

        private HashSet<DrawingModel> selectedItems = new HashSet<DrawingModel>();
        private SortDirection _sortDirection = SortDirection.None;

        private TableState _tableState = new();
        private MudTable<DrawingModel> tableRef;

        private List <DrawingModel> _drawings= new ();
        private List<string> insertErrors = new ();


        private string _toNewDrawing = "/sostav/newdrawing";


        private string searchString;

        string styleTableHeader = "font-size: 12px; text-align: center; padding: 0 0 0 10px; overflow-wrap: break-word; line-height: 1;";
        string styleTableBody = "padding: 0; text-align: center;";


        protected override async Task OnInitializedAsync()
        {
           _drawings = await EntityManagementService.GetDrawingModelsByProjectIdAsync(ProjectID);

        }


        private async Task<List<DrawingModel>> GetDrawingsWithoutEstimates()
        {
            _drawings.Clear();
            _drawings = await EntityManagementService.GetDrawingModelsByProjectIdAsync(ProjectID);
            return _drawings;
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
            var currentDrawing = tableRowClickEventArgs.Item.DrawingId;
            if (await EntityManagementService.EditDrawingDialogAsync(currentDrawing))
            {
                //Snackbar.Add(Localizer["drawingEdited"], Severity.Success);
                await GetDrawingsWithoutEstimates();
                await tableRef.ReloadServerData();
            }
            else
            {
                Snackbar.Add(Localizer["drawingNotEdited"], Severity.Error);
            }

            selectedItems.Clear();
            StateHasChanged();

        }



        // Обработчик сортировки
        private async Task OnSort(TableState state)
        {
            _tableState = state;
            _drawings = await GetSortedData(state);
            tableRef.ReloadServerData();
        }
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

            }
            else
            {
                Snackbar.Add(Localizer["noItems"], Severity.Info);
            }
            await GetDrawingsWithoutEstimates();
            selectedItems.Clear();
        }
        private async Task CopyDrawings()
        {
            insertErrors.Clear();
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

                foreach (var drawing in _drawingsForCopy)
                {
                    if(await EntityManagementService.AddSingleDrawingAsync(drawing) == 0)
                    {
                        insertErrors.Add(drawing.DrawingName);
                    }
                }
                if (insertErrors.Count == 0)
                {

                        await GetDrawingsWithoutEstimates();
                        await tableRef.ReloadServerData();

                    Snackbar.Add("Copied", Severity.Success);
                }
                else
                {
                    Snackbar.Add("Not copied", Severity.Error);
                }
            }
            selectedItems.Clear ();
            StateHasChanged();

        }

        //private async Task OpenFile()
        //{
        //    string fileUrl = @"d:\Elena\Recovery.txt";
        //    if (fileUrl != null)
        //    {
        //        // Используем JSRuntime для открытия файла в новом окне
        //        await JSRuntime.InvokeVoidAsync("open", fileUrl, "_blank");

        //    }
  

        //}
    }
}

