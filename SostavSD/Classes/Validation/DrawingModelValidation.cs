using SostavSD.Models;

namespace SostavSD.Classes.Validation
{
    public class DrawingModelValidation
    {
        public ValidationResult Validate(DrawingModel drawing)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(drawing.DrawingName))
            {
                result.Errors.Add("Drawing Name is empty");
            }

            return result;
        }
    }
}
