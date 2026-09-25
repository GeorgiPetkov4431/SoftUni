using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Utilities
{
    public class ObjectValidator
    {
        public static bool IsValid(object obj, out List<string> validationErrors)
        {
            validationErrors = new List<string>();

            ValidationContext validationContext = new ValidationContext(obj);
            ICollection<ValidationResult> validationResult = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            if (!isValid)
            {
                foreach (ValidationResult validationResultItem in validationResult)
                {
                    validationErrors.Add(validationResultItem.ErrorMessage ?? "Unknown validation error.");
                }
            }

            return isValid;
        }
    }
}
