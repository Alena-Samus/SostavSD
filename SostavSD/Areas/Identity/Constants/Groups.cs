using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SostavSD.Areas.Identity.Constants
{
    public enum Groups
    {
        [Display(Name ="Электрики")]
        electricians = 1,

        [Display(Name = "Строители")]
        builders,
        [Display(Name = "Сантехники")]
        plumbers,
        [Display(Name = "Технологи")]
        technologists,
        [Display(Name = "Тепловые сети")]
        heatingnetwork,
        [Display(Name = "Расчетная")]
        calculated,
        [Display(Name = "Руководство")]
        management,
        [Display(Name = "ГИП")]
        cpe,
    }
}
