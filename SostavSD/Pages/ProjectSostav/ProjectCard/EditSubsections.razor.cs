using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;

namespace SostavSD.Pages.ProjectSostav.ProjectCard
{
    public partial class EditSubsections
    {
        [Inject] IChapterService ChapterService { get; set; }
        [Inject] IStringLocalizer<AddNewSubsections> Localizer { get; set; }
        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Parameter] public int ProjectId { get; set; }


    }
}
