using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SostavSD.Models;

namespace SostavSD.Pages.ProjectSostav
{
    partial class NewDrawingsList
    {
        [Parameter] public List<DrawingModel> ListNewDrawings { get; set; }
        [Inject] IStringLocalizer<NewDrawingsList> Localizer { get; set; }

    }

}
