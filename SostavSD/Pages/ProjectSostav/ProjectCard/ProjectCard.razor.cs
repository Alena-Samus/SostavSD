using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MimeKit.Cryptography;
using MudBlazor;
using SostavSD.Classes.ProjectChapter;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Runtime.CompilerServices;

namespace SostavSD.Pages.ProjectSostav.ProjectCard
{
    public partial class ProjectCard
    {
        [Parameter] public int ProjectId { get; set; }

        [Inject] IProjectService ProjectService { get; set; }
        [Inject] ISubsectionService SubsectionService { get; set; }
        [Inject] ICoefficientService CoefficientService { get; set; }
        [Inject] IEditService EntityManagementService { get; set; }
        [Inject] public IAuthorizedUserService AuthorizedUserService { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] IStringLocalizer<ProjectCard> Localizer { get; set; }



        private List<CoefficientModel> coefficients = new List<CoefficientModel>();
        private List<SubsectionModel> subsections = new List<SubsectionModel>();
        private List<ChapterModel> chapters = new List<ChapterModel>();
        private List<ProjectChapter> projectChapters = new List<ProjectChapter>();


        private ProjectModel _projectModel = new();
        string calculatorName;
        private int? buildingViewId;
        private int? buildingZoneId;

        protected override async Task OnInitializedAsync()
        {
            await GetProject();

            ManagerUserModel _calculatorName = await AuthorizedUserService.GetSingleUser(_projectModel.Contract.CalculatorId);
            if (_calculatorName != null)
            {
                calculatorName = _calculatorName.UserSurname;
            }

            await GetChapters();
            

            await GetCoefficients();
        }

        private async Task GetProject()
        {
            _projectModel = await ProjectService.GetProjectByIdAsync(ProjectId);
            buildingViewId = _projectModel.BuildingViewId;
            buildingZoneId = _projectModel.Contract.BuildingZoneId;

        }
        private async Task GetChapters()
        {
            subsections = await SubsectionService.GetSubsectionByProjectIdAsync(ProjectId);
            var chapterList = subsections.DistinctBy(p => p.ChapterId).OrderBy(p => p.ChapterId).ToList();
            foreach (var chapter in chapterList)
            {
                chapters.Add(chapter.Chapter);
            }

            foreach (var chapter in chapters)
            {
                List<SubsectionModel> _currentSubsections = subsections.FindAll(item => item.ChapterId == chapter.ChapterId);
                ProjectChapter _subsection = new ProjectChapter()
                {
                    Chapter = chapter,
                    Subsections = _currentSubsections.OrderBy(p => p.SerialNumber).ToList(),
                };
                projectChapters.Add(_subsection);
            }
        }

        private async Task GetCoefficients()
        {

            if (buildingViewId != null && buildingZoneId != null)
            {
                coefficients = await CoefficientService.GetCoefficiensByBuildingViewIdBuildingZoneId((int)buildingViewId, (int)buildingZoneId);

            }
            foreach (var coefficient in coefficients)
            {
                coefficient.OHROPR1 = Math.Round((coefficient.OHROPR1 * _projectModel.ProjectK1) ?? 0, 2);
                coefficient.OHROPR2 = Math.Round((coefficient.OHROPR2 * _projectModel.ProjectK2) ?? 0, 2);
            }

        }

        private async Task UpdateCoefficients()
        {
            coefficients.Clear();
            await GetProject();
            await GetCoefficients();
        }
        private async Task ChangeCiC()
        {
            var parameters = new DialogParameters();

            parameters.Add("ProjectCiC", _projectModel.CiCVersion);
            var dialog = await DialogService.Show<ChangeCiCDialog>("Edit", parameters).Result;
            if (dialog.Data != null)
            {
                _projectModel.CiCVersion = (string)dialog.Data;
                if (await ProjectService.UpdateCiCVersionAsync(_projectModel.ProjectId, _projectModel.CiCVersion))
                {
                    Snackbar.Add(@Localizer["changed"], Severity.Success);
                }
                else
                {
                    Snackbar.Add(@Localizer["notchanged"], Severity.Error);
                }
            }
        }

        private void NavigateToPage()
        {
            _navigationManager.NavigateTo($"/projectcard/addnewsubsections/{ProjectId}");
        }

        private async Task RemoveChapters()
        {
            if (await SubsectionService.RemoveSubsectionsByProjectIdAsync(ProjectId))
            {
                Snackbar.Add(Localizer["projectChaptersIsRemoved"], Severity.Success);
                await GetChapters();
                projectChapters.Clear();
                StateHasChanged();
            }
            else
            {
                Snackbar.Add(Localizer["projectChaptersNotRemoved"], Severity.Error);
            }

        }

        private async Task CopyChapters()
        {
            if (projectChapters.Count != 0)
            {
                Snackbar.Add(Localizer["projectChaptersIsNotEmpty"], Severity.Error);
            }
            else
            {
                var parameters = new DialogParameters();
                parameters.Add("BuildingNumber", string.Empty);

                var dialog = await DialogService.Show<CopyChaptersDialog>("Copy", parameters).Result;

                if (dialog.Data != null)
                {
                    int _newProjectId = await ProjectService.GetPtojectIdAsync(dialog.Data.ToString());
                    List<SubsectionModel> _subsectionSource = await SubsectionService.GetSubsectionByProjectIdAsync(_newProjectId);
                    foreach (var item in _subsectionSource)
                    {
                        SubsectionModel _currentProjectSubsection = new SubsectionModel()
                        {
                            SerialNumber = item.SerialNumber,
                            SubsectionName = item.SubsectionName,
                            ChapterId = item.ChapterId,
                            ProjectId = ProjectId,
                            K1 = item.K1,
                            K2 = item.K2,
                            Norm = item.Norm,
                        };
                        subsections.Add(_currentProjectSubsection);
                    }
                    await SubsectionService.AddSubsectionsAsync(subsections);

                    Snackbar.Add(Localizer["projectChaptersIsCopied"], Severity.Success);
                    subsections.Clear();
                }
                await GetChapters();
                StateHasChanged();
            }
        }


        private async Task CopySubsection(int subsectionId)
        {
            var _currentSubsection = await SubsectionService.GetSubsectionById(subsectionId);
            SubsectionModel subsection = new SubsectionModel()
            {
                SubsectionName = $"{_currentSubsection.SubsectionName} Копия",
                ChapterId = _currentSubsection.ChapterId,
                ProjectId = _currentSubsection.ProjectId,
                K1 = _currentSubsection.K1,
                K2 = _currentSubsection.K2,
                Norm = _currentSubsection.Norm,
                Notes = _currentSubsection.Notes,
            };
            List<SubsectionModel> _currentList = new List<SubsectionModel>();
            _currentList.Add(subsection);
            if (await SubsectionService.AddSubsectionsAsync(_currentList))
            {
                Snackbar.Add(Localizer["subsectionIsCopied"], Severity.Success);
                await GetChapters();
                StateHasChanged();
            }
            else
            {
                Snackbar.Add(Localizer["subsectionNotCopied"], Severity.Error);
            }
            subsections.Clear();
            projectChapters.Clear();
            await GetChapters();
            StateHasChanged();
        }
        private async Task RemoveSubsection(int subsectionId)
        {
            if (await SubsectionService.RemoveSubsectionsByIdAsync(subsectionId))
            {
                Snackbar.Add(Localizer["subsectionIsRemoved"], Severity.Success);
                await GetChapters();
                StateHasChanged();
            }
            else
            {
                Snackbar.Add(Localizer["subsectionNotRemoved"], Severity.Error);
            }
            subsections.Clear();
            projectChapters.Clear();
            await GetChapters();
            StateHasChanged();
        }

        private async Task EditSubsection(int subsectionId)
        {
            if (await EntityManagementService.EditSubsectionDialogAsync(subsectionId))
            {
                Snackbar.Add(Localizer["subsectionIsEdited"], Severity.Success);

                subsections.Clear();
                projectChapters.Clear();
                await GetChapters();
            }
            else
            {
                Snackbar.Add(Localizer["subsectionNotEdited"], Severity.Error);
            }

        }


    }
}
