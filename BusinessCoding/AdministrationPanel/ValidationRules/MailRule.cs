using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace AdministrationPanel.ValidationRules
{
    class MailRule : ValidationRule
    {
        public MailRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string mail = (string)value;

            if (string.IsNullOrEmpty(mail))
            {
                return new ValidationResult(false, "Mail cannot be empty");
            }

            try
            {
                var tmp = new System.Net.Mail.MailAddress(mail);
            }
            catch
            {
                return new ValidationResult(false, "Mail is wrong");
            }
            
            return new ValidationResult(true, null);
        }
    }
}
