using System;
using System.Globalization;
using System.Windows.Controls;

namespace Spectre.DivikWpfClient.Validation
{
    public class IntegerRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int num = 0;

            if (!int.TryParse(value.ToString(), out num))
                return new ValidationResult(false, String.Format("Please enter an integer value."));

            return new ValidationResult(true, null);
        }
    }
}
