using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.VisualBasic;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSostav
{
    partial class NewDrawing
    {
        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] IStringLocalizer<DrawingsWithoutEstimates> Localizer { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        private NavigationManager _navigationManager;

        private DrawingModel _newDrawing = new();


        List<DrawingModel> _newDrawingsList = new List<DrawingModel>();

        protected override async Task OnInitializedAsync()
        {
            _newDrawing = new DrawingModel();
        }
        public NewDrawing(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        private void Save()
        {
            Snackbar.Add("Saved", Severity.Success);
            GoBack();
        }

        private void GoBack()
        {
            Snackbar.Add("Canceled", Severity.Warning);
            _navigationManager.NavigateTo("javascript:history.back()", forceLoad: true);
        }
    }
}
