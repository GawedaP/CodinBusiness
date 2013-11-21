using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace AdministrationPanel.ValidationRules
{
    class SalaryRule : ValidationRule
    {
        public double MaxSalary
        {
            get;
            set;
        }

        public double MinSalary
        {
            get;
            set;
        }

        public SalaryRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double number = 0;

            try
            {
                if (((string)value).Length > 0)
                {
                    number = Double.Parse((String)value);
                }
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((number < MinSalary) || (number > MaxSalary))
            {
                return new ValidationResult(false, "Please enter a number in the range: " + MinSalary + " - " + MaxSalary + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
