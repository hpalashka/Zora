using System;
using System.ComponentModel.DataAnnotations;
using Zora.Shared.Data;

namespace Zora.Commons.Helpers
{
    public class DateGreaterThan : ValidationAttribute
    {
        private string _startDatePropertyName;
        public DateGreaterThan(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_startDatePropertyName);
            var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (value != null && propertyValue != null)
            {
                if ((DateTime)value > (DateTime)propertyValue)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ValidationConstants.DateGreaterThan);
                }
            }
            else 
            {
                return new ValidationResult(ValidationConstants.RequiredFieldEmpty);
            }
        }
    }
}
