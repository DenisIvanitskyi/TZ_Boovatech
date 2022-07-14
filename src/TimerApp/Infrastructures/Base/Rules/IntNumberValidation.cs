using System.Globalization;
using System.Windows.Controls;

namespace TimerApp.Infrastructures.Base.Rules
{
    public class IntNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (double.TryParse(value?.ToString(), out var number))
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "Input value must be integer number");
        }
    }
}
