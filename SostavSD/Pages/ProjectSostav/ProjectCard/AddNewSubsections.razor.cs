using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Reflection.Metadata;

namespace SostavSD.Pages.ProjectSostav.ProjectCard
{
    public partial class AddNewSubsections
    {
        [Inject] IChapterService ChapterService { get; set; }
        [Inject] IStringLocalizer<AddNewSubsections> Localizer { get; set; }
        [Parameter] public int ProjectId { get; set; }

        private List<ChapterModel> chapters = new List<ChapterModel>();
        
        private ChapterModel Chapter = new ChapterModel();
        private string Country { get; set; } = "РБ";
        private int SelectedChapter { get; set; }
       
        
        protected override async Task OnInitializedAsync()
        {
            chapters = await ChapterService.GetChaptersByCountryAsync(Country);
            //Chapter = new ChapterModel();
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

        private async Task OnGroupSelected(int selectedChapter)
        {
            //Chapter = chapters.FirstOrDefault(e => e.ChapterId == selectedChapter);
            //SelectedChapter = Chapter.ChapterName;
        }

    }
}
