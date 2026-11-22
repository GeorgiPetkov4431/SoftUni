using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Services.Common.Utilities
{
    public class BasicModelValidator
    {
        private readonly IList<ValidationResult> _validationResults;

        public BasicModelValidator()
        {
            this._validationResults = new List<ValidationResult>();
        }

        public IReadOnlyCollection<ValidationResult> ValidationResults
            => this._validationResults.AsReadOnly();

        public bool IsValid(object model)
        {
            var validationContext = new ValidationContext(model);

            return Validator
                .TryValidateObject(model, validationContext, this._validationResults, true);
        }

        public static bool IsValid(object model, List<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(model);

            return Validator
                .TryValidateObject(model, validationContext, validationResults, true);
        }
    }
}
