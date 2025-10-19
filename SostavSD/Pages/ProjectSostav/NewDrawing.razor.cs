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



        private List<DrawingModelForList> newList = new List<DrawingModelForList> ();
        private List<DrawingModel> listForSave = new List<DrawingModel> ();
        private List<DeppartModel> _groupForTable = new List<DeppartModel>();
        private List<string> _groups = new List<string>() { "1", "2", "3", "4" };


        private NavigationManager navigationManager;
        private DrawingModelValidation validation = new();
        private DrawingModel newDrawing = new();

        string groupName;

        protected override async Task OnInitializedAsync()
        {
            _groupForTable = await EntityManagementService.GetDeppartsByGroupsAsync(_groups);
            newDrawing = new DrawingModelForList();
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
                PrepareForSave();
                if (await EntityManagementService.AddDrawingsAsync(listForSave))
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

        private void PrepareForSave()
        {
            foreach(var item in newList)
            {
                DrawingModel _currentDrawingForSave = new DrawingModel()
                {
                    DrawingName = item.DrawingName,
                    ProjectId = item.ProjectId,
                    DrawingDateOfAdmissionToDepartment = item.DrawingDateOfAdmissionToDepartment,
                    DrawingReleaseDateBySchedule = item.DrawingReleaseDateBySchedule,
                    DrawingReleaseDateDepertment = item.DrawingReleaseDateDepertment,
                    GroupId = item.GroupId,
                };
                listForSave.Add(_currentDrawingForSave);
            }

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
            DrawingModelForList _currentDrawing = new DrawingModelForList 
                                           { 
                                              DrawingName = newDrawing.DrawingName, 
                                              ProjectId = newDrawing.ProjectId,
                                              DrawingDateOfAdmissionToDepartment = newDrawing.DrawingDateOfAdmissionToDepartment,
                                              DrawingReleaseDateBySchedule = newDrawing.DrawingReleaseDateBySchedule,
                                              DrawingReleaseDateDepertment = newDrawing.DrawingReleaseDateDepertment,
                                              GroupId = newDrawing.GroupId,
                                              DeppartName = groupName,
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

        public void DeleteTheItem (DrawingModelForList drawingModel)
        {
            newList.Remove(drawingModel);
        }

        private async Task OnGroupSelected(int? selectedGroupId)
        {
            newDrawing.GroupId = selectedGroupId;

            if (selectedGroupId.HasValue)
            {
                var selectedGroup = _groupForTable.FirstOrDefault(g => g.GroupId == selectedGroupId.Value);
                if (selectedGroup != null)
                {
                    groupName = selectedGroup.GroupName;
                }
            }
            else
            {
                groupName = null;
            }
        }
    }
}

