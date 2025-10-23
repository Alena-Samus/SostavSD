using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Reflection.Metadata;

namespace SostavSD.Pages.ProjectSostav.ProjectCard
{
    public partial class AddNewSubsections
    {
        [Inject] IChapterService ChapterService { get; set; }
        [Inject] IStringLocalizer<AddNewSubsections> Localizer { get; set; }
        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Parameter] public int ProjectId { get; set; }

        private List<ChapterModel> chapters = new List<ChapterModel>();
        private List<SubsectionModel> currenSubsections = new List<SubsectionModel>();
        
        private ChapterModel chapter = new ChapterModel();
        private SubsectionModel subsection = new SubsectionModel();

        private MudTable<SubsectionModel> tableRef;
        private string Country { get; set; } = "РБ";
        private int SelectedChapter { get; set; }

        private NavigationManager navigationManager;



        protected override async Task OnInitializedAsync()
        {
            chapters = await ChapterService.GetChaptersByCountryAsync(Country);

        }
        public AddNewSubsections(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }
        private async Task OnSelectedOptionChanged(string selectedOption)
        {
            Country = selectedOption;
            await GetChapters(Country);
            // call your stuff
        }

        private async Task GetChapters (string country)
        {
            chapters = await ChapterService.GetChaptersByCountryAsync(country);
        }

        private void OnGroupSelected(ChapterModel selectedChapter)
        {
           chapter.ChapterId = selectedChapter.ChapterId;
           chapter.ChapterName = selectedChapter.ChapterName;
        }

        private void Cancel()
        {
            navigationManager.NavigateTo("javascript:history.back()", forceLoad: true);

        }
        private void Clear()
        {
            currenSubsections.Clear();
        }
        private async Task Save()
        {
            if (currenSubsections.Count > 0)
            {
                await EntityManagementService.AddSubsectionsAsync(currenSubsections);
                Snackbar.Add(Localizer["chapterAdded"], Severity.Success);
                currenSubsections.Clear();
            }
            else
            {
                Snackbar.Add(Localizer["noItemsInChapter"], Severity.Error);

            }
        }
        private void AddItem()
        {
          SubsectionModel  _currentSubsection = new SubsectionModel()
            {
                SerialNumber = subsection.SerialNumber,
                SubsectionName =subsection.SubsectionName,
                ProjectId = ProjectId,
                ChapterId = chapter.ChapterId,
                K1 = subsection.K1,
                K2 = subsection.K2,
                Norm = subsection.Norm,
                Notes = subsection.Notes,
            };

            currenSubsections.Add(_currentSubsection);
            ClearSubsection();
            
        }

        private void ClearSubsection()
        {
            subsection.SerialNumber = null;
            subsection.SubsectionName = null;
            subsection.K1 = null;
            subsection.K2 = null;
            subsection.Norm = null;
            subsection.Notes = null;
        }

        private void RemoveItem(SubsectionModel subsection)
        {
            currenSubsections.Remove(subsection);
        }

    }
}
