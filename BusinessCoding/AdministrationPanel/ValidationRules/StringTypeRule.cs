using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;


namespace AdministrationPanel.ValidationRules
{
    class StringTypeRule : ValidationRule
    {
        public StringTypeRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "String cannot be empty");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }

    
}
