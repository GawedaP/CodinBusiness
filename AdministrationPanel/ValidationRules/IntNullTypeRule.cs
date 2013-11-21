using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace AdministrationPanel.ValidationRules
{
    class IntNullTypeRule : ValidationRule
    { 
        public int MinValue
        {
            get;
            set;
        }

        public int MaxValue
        { 
            get; 
            set; 
        }

        public IntNullTypeRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int number = 0;

            try
            {
                if ((string)value == null)
                {
                    return new ValidationResult(true, null);
                }

                else if (((string)value).Length == 0)
                {
                    return new ValidationResult(true, null);
                }

                else if (((string)value).Length > 0)
                {
                    number = int.Parse((String)value);
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
