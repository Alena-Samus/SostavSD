using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSostav
{
    partial class DrawingsWithoutEstimates
    {
        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] IStringLocalizer<DrawingsWithoutEstimates> Localizer { get; set; }

        List <DrawingModel> _drawings= new ();

        protected override async Task OnInitializedAsync()
        {
           _drawings = await EntityManagementService.GetDrawingModelsAsync();
        }

        void CommitedItemChanges(DrawingModel item)
        {
            EntityManagementService.EditDrawing(item);
        }
    }
}
