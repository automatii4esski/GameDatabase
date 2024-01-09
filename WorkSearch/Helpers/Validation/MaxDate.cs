using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WorkSearch.Helpers.Messages;

namespace WorkSearch.Helpers.Validation
{
    public class MaxDate : ValidationAttribute, IClientModelValidator
    {
        public DateTime Date;
        public new string ErrorMessage;

        public MaxDate(string date)
        {
            Date = DateTime.Parse(date) ;
            ErrorMessage = String.Format(ErrorMessage ?? ErrorMessages.MaxDate, Date);
        }

        public MaxDate()
        {
            Date =  DateTime.Now;
            ErrorMessage = String.Format(ErrorMessage ?? ErrorMessages.MaxDate, Date);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if(context.Attributes.ContainsKey("data-val")) context.Attributes.Add("data-val", "true");
            
            context.Attributes.Add("data-val-maxdate", ErrorMessage);
            context.Attributes.Add("data-val-maxdate-date", Date.ToString("dd-MM-yyyy"));
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (!(value is DateTime)) return new ValidationResult(ErrorMessages.InvalidType);

            if ((DateTime)value > Date) return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
