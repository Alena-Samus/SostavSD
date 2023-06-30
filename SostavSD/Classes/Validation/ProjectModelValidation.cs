using Microsoft.AspNetCore.Components;
using SostavSD.Interfaces;
using SostavSD.Models;


namespace SostavSD.Classes.Validation
{
    public class ProjectModelValidation
    {
        public ValidationResult Validate(ProjectModel project)
        {
            var result = new ValidationResult();

            if (project.ProjectReleaseDate > project.ProjectReleaseDateByContract)
            {
                result.Errors.Add("Дата выпуска больше, чем дата выпуска по договору");
            }
            if (string.IsNullOrWhiteSpace(project.BuildingNumber))
            {
                result.Errors.Add("Поле с номером стройки не заполнено!");
            }

            return result;
        }

       
    }
}
