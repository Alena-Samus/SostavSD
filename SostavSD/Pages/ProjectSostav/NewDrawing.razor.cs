using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SostavSD.Areas.Identity.Constants;
using Microsoft.VisualBasic;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSostav
{
    partial class NewDrawing
    {
        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] IStringLocalizer<NewDrawing> Localizer { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        [Parameter] public int ProjectID { get; set; }

        private NavigationManager navigationManager;

        private DrawingModel newDrawing = new();
        private Groups group;

        private List<DrawingModel> newList = new List<DrawingModel> ();

        protected override async Task OnInitializedAsync()
        {
            newDrawing = new DrawingModel();
            newDrawing.ProjectId = ProjectID;
            newDrawing.DrawingDateOfAdmissionToDepartment = DateTime.Now;
        }
        public NewDrawing(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }

        private void Save()
        {
            Snackbar.Add("Saved", Severity.Success);
            GoBack();
        }

        private void GoBack()
        {
            Snackbar.Add("Canceled", Severity.Warning);
            navigationManager.NavigateTo("javascript:history.back()", forceLoad: true);
        }
        private void AddItem()
        {
            DrawingModel _currentDrawing = new DrawingModel 
                                           { 
                                              DrawingName = newDrawing.DrawingName, 
                                              DrawingDateOfAdmissionToDepartment = newDrawing.DrawingDateOfAdmissionToDepartment,
                                              DrawingReleaseDateBySchedule = newDrawing.DrawingReleaseDateBySchedule,
                                              DrawingReleaseDateDepertment = newDrawing.DrawingReleaseDateDepertment
                                            };
            newList.Add(_currentDrawing);
            ClearNewDrawing();
            StateHasChanged();
        }

        private void ClearNewDrawing()
        {
            newDrawing.DrawingName = string.Empty;
            newDrawing.DrawingReleaseDateDepertment = null;
            newDrawing.DrawingReleaseDateBySchedule = null;
        }

        public void DeleteTheItem (DrawingModel drawingModel)
        {
            newList.Remove(drawingModel);
        }
    }
}
