using System.Globalization;
using System.Windows.Controls;

namespace TimerApp.Infrastructures.Base.Rules
{
    public class RangeNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (double.TryParse(value?.ToString(), out var number) && number >= 0 && number <= 72000)
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "Input value should be from 0 to 7200 seconds (20 hours)");
        }
    }
}
