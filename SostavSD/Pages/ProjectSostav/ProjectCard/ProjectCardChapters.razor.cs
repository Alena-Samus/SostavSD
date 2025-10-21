using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Classes.ProjectChapter;
using SostavSD.Interfaces;
using SostavSD.Models;

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
            subsections = await EntityManagementService.GetSubsectionByProjectIdAsync(ProjectId);
            GetChapters(subsections);

        }
        private void GetChapters(List<SubsectionModel> subsections)
        {
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
                                                 Subsections = _currentSubsections
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

        private void CopyChapters()
        {

        }
        
    }
}
