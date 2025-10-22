using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Classes.ProjectChapter;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Pages.Projects;

namespace SostavSD.Pages.ProjectSostav.ProjectCard
{
    public partial class ProjectCardChapters
    {
        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] IStringLocalizer<ProjectCardChapters> Localizer { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] IDialogService _dialogService { get; set; }
        [Parameter] public int ProjectId { get; set; }

        private List<SubsectionModel> subsections = new List<SubsectionModel>();
        private List<ChapterModel> chapters = new List<ChapterModel>();
        private List<ProjectChapter> projectChapters = new List<ProjectChapter>();


        private MudTable<SubsectionModel> tableRef;

        protected override async Task OnInitializedAsync()
        {
            
           await  GetChapters();

        }
        private async Task  GetChapters()
        {
            subsections = await EntityManagementService.GetSubsectionByProjectIdAsync(ProjectId);
            var chapterList = subsections.DistinctBy(p => p.ChapterId).OrderBy(p => p.ChapterId).ToList();
            foreach (var chapter in chapterList) 
            {
                chapters.Add(chapter.Chapter);
            }

            foreach (var chapter in chapters)
            {
                List<SubsectionModel> _currentSubsections = subsections.FindAll(item => item.ChapterId == chapter.ChapterId);
                ProjectChapter _subsection = new ProjectChapter() {
                                                 Chapter = chapter,
                                                 Subsections = _currentSubsections.OrderBy(p => p.SerialNumber).ToList(),
                                                 };
                projectChapters.Add(_subsection);
            }
        }
        private void NavigateToPage()
        {

        }

        private void RemoveChapters()
        {

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

                var dialog = await _dialogService.Show<CopyChaptersDialog>("Copy", parameters).Result;

                if (dialog.Data != null)
                {
                    int _newProjectId = await EntityManagementService.GetPtojectIdAsync(dialog.Data.ToString());
                    List<SubsectionModel> _subsectionSource = await EntityManagementService.GetSubsectionByProjectIdAsync(_newProjectId);
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
                    await EntityManagementService.AddSubsectionsAsync(subsections);

                    Snackbar.Add(Localizer["projectChaptersIsCopied"], Severity.Success);
                    subsections.Clear();
                }
                await GetChapters();
                StateHasChanged();
                //await tableRef.ReloadServerData();
            }
        }

  


        private void EditChapters()
        {

        }
    }
}
