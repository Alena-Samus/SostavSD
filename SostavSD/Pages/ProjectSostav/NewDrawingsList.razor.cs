using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSostav
{
    partial class NewDrawingsList
    {
        [Inject] IStringLocalizer<NewDrawingsList> Localizer { get; set; }

        [Parameter] public List<DrawingModel> ListNewDrawings { get; set; }
        [Parameter] public EventCallback<DrawingModel> DeleteItem { get; set; }



    }

}
