using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSostav
{
    partial class NewDrawingsList
    {
        [Inject] IStringLocalizer<NewDrawingsList> Localizer { get; set; }

        [Parameter] public List<DrawingModelForList> ListNewDrawings { get; set; }
        [Parameter] public EventCallback<DrawingModelForList> DeleteItem { get; set; }



    }

}
