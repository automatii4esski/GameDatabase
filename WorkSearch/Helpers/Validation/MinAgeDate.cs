using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using WorkSearch.Helpers.Messages;

namespace WorkSearch.Helpers.Validation
{
    public class MinAgeDate : ValidationAttribute, IClientModelValidator
    {
        public int MinAge { get; set; }
        public new string ErrorMessage;

        public MinAgeDate(int minAge)
        {
            MinAge = minAge;
            ErrorMessage = String.Format(ErrorMessage ?? ErrorMessages.MinimumAgeRequirements, MinAge);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (!context.Attributes.ContainsKey("data-val")) context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-minagedate", ErrorMessage);
            context.Attributes.Add("data-val-minagedate-minage", MinAge.ToString());
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (!(value is DateTime)) return new ValidationResult(ErrorMessages.InvalidType);

            if (DateHelper.GetAge((DateTime)value) < MinAge) return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
