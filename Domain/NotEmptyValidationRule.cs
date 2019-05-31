using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TaskManager.Domain
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrEmpty(value as string)
                ? new ValidationResult(false, "Pole jest wymagane")
                : ValidationResult.ValidResult;
        }
    }
}
