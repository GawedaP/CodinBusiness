using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace AdministrationPanel.ValidationRules
{
    class DateTimeNullTypeRule : ValidationRule
    {
        public DateTime MinDate
        {
            get;
            set;
        }

        public DateTime MaxDate
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime date = DateTime.Now;

            try
            {
                if (((string)value) == null)
                {
                    return new ValidationResult(true, null);
                }
                else if (((string)value).Length == 0)
                {
                    return new ValidationResult(true, null);
                }
                else if (((string)value).Length > 0)
                {
                    date = DateTime.Parse((String)value);
                }
            }
            catch (Exception e)
            {
                
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((date < MinDate) || (date > MaxDate))
            {
                return new ValidationResult(false, "Please enter a date in the range: " + MinDate + " - " + MaxDate + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
