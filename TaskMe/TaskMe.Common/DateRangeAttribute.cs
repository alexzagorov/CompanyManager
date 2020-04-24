namespace TaskMe.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // my validation logic
            if ((DateTime)value >= DateTime.UtcNow.Date && (DateTime)value <= DateTime.UtcNow.AddYears(2).Date)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date can not be before today and more than 2 years ahead.");
            }
        }
    }
}
