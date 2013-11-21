using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace AdministrationPanel.ValidationRules
{
    class DoubleTypeRule: ValidationRule
    {
        public double MinValue
        {
            get;
            set;
        }

        public double MaxValue
        { 
            get; 
            set; 
        }

        public DoubleTypeRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double number = 0;

            try
            {
                if (((string)value).Length > 0)
                {
                    number = double.Parse((String)value);
                }
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((number < MinValue) || (number > MaxValue))
            {
                return new ValidationResult(false, "Please enter a number in the range: " + MinValue + " - " + MaxValue + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
