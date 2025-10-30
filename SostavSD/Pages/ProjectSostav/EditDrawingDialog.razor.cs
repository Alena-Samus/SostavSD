using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Classes.Validation;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Services;

namespace SostavSD.Pages.ProjectSostav
{
    public partial class EditDrawingDialog: ComponentBase
    {
        [Inject] IDeppartService DeppartService { get; set; }
        [Inject] IAuthorizedUserService AuthorizedUserService { get; set; }
        [Inject] IStringLocalizer<EditDrawingDialog> Localizer { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        [CascadingParameter] MudDialogInstance EditDrawing { get; set; }
        [Parameter] public DrawingModel Drawing { get; set; }

        private List<GroupHeadsModel> groupHeads { get; set; } = new List<GroupHeadsModel>();
        private List<DeppartModel> _groupForTable = new List<DeppartModel>();
        private List<string> _groups = new List<string>() { "1", "2", "3", "4" };
        private string estimator;
        protected override async Task OnInitializedAsync()
        {
            groupHeads = await AuthorizedUserService.GetGroupHeadsAsync();
            _groupForTable = await DeppartService.GetDeppartsByGroupsAsync(_groups);
            AddToTheGroupHeads(groupHeads, _groupForTable);
        }

        private void AddToTheGroupHeads(List<GroupHeadsModel> heads, List<DeppartModel> depparts)
        {
            foreach (var group in heads)
            {
                var _currentGroup = depparts.FirstOrDefault(g => g.GroupANU == group.GroupANU);
                group.GroupId = _currentGroup.GroupId;
                group.GroupName = _currentGroup.GroupName;               
            }
        }

        private void Cancel()
        {
            EditDrawing.Close();
            Snackbar.Add(Localizer["editingCanceled"], Severity.Info);

        }

        private void Submit()
        {
            //var validationResult = _contractModelValidation.Validate(Contract);
            //string errors = string.Empty;
            //if (validationResult.IsValid)
            //{
                EditDrawing.Close(DialogResult.Ok(Drawing));
                Snackbar.Add(Localizer["drawingEdited"], Severity.Success);
            //}
            //else
            //{
            //    foreach (var item in validationResult.Errors)
            //    {
            //        errors += $"{item} ";
            //    }
            //    Snackbar.Add($"{errors}", Severity.Error);
            //}

        }

        private void OnGroupSelected(DeppartModel selectedGroup)
        {
            Drawing.GroupId = selectedGroup.GroupId;

            if (selectedGroup != null)
            {
                var _selectedGroup = groupHeads.FirstOrDefault(g => g.GroupId == selectedGroup.GroupId);
                if (selectedGroup != null)
                {
                    
                    estimator = _selectedGroup.SurnameHeadOfGroup;
                   
                }
            }
            else
            {
                estimator = null;
            }
        }

    }
}
