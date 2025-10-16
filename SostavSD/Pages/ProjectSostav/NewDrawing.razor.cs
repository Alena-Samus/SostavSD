using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SostavSD.Areas.Identity.Constants;
using Microsoft.VisualBasic;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Threading.Tasks;
using SostavSD.Classes.Validation;

namespace SostavSD.Pages.ProjectSostav
{
    partial class NewDrawing
    {
        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] IStringLocalizer<NewDrawing> Localizer { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        [Parameter] public int ProjectID { get; set; }


        private List<DrawingModel> newList = new List<DrawingModel> ();

        private NavigationManager navigationManager;
        private DrawingModelValidation validation = new();
        private DrawingModel newDrawing = new();
        private Groups group;

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

        private async Task Save()
        {

            if (newDrawing != null)
            {
                if (newList == null || newList.Count == 0)
                {
                    AddItem();
                }
                if (await EntityManagementService.AddDrawingsAsync(newList))
                {
                    Snackbar.Add(Localizer["saved"], Severity.Success);
                }

            }
            else 
            {
                Snackbar.Add(Localizer["noData"], Severity.Error);
            }
                GoBack();
        }
         private void Cancel()
        {
            GoBack();
            Snackbar.Add(Localizer["canceled"], Severity.Warning);

        }
        private void GoBack()
        {
            navigationManager.NavigateTo("javascript:history.back()", forceLoad: true);
        }
        private void AddItem()
        {
            DrawingModel _currentDrawing = new DrawingModel 
                                           { 
                                              DrawingName = newDrawing.DrawingName, 
                                              ProjectId = newDrawing.ProjectId,
                                              DrawingDateOfAdmissionToDepartment = newDrawing.DrawingDateOfAdmissionToDepartment,
                                              DrawingReleaseDateBySchedule = newDrawing.DrawingReleaseDateBySchedule,
                                              DrawingReleaseDateDepertment = newDrawing.DrawingReleaseDateDepertment
                                            };

            var validationResult = validation.Validate(_currentDrawing);
            if (validationResult.IsValid)
            {
                newList.Add(_currentDrawing);
            }
            else 
            { 
                Snackbar.Add(Localizer["noData"], Severity.Error);
            }            

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
