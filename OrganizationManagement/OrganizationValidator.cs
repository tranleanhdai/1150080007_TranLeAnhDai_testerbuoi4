using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace OrganizationManagement
{
    public static class OrganizationValidator
    {
        // Spec (từ đề):
        // - OrgName: required, length 3..255
        // - Phone (nếu có): chỉ số, dài 9..12
        // - Email (nếu có): đúng định dạng email
        public static Dictionary<string, string> Validate(string orgName, string address, string phone, string email)
        {
            var errors = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            var name = (orgName ?? string.Empty).Trim();
            var ph = (phone ?? string.Empty).Trim();
            var em = (email ?? string.Empty).Trim();

            // OrgName
            if (string.IsNullOrWhiteSpace(name))
            {
                errors["OrgName"] = "Organization name is required";
            }
            else if (name.Length < 3 || name.Length > 255)
            {
                errors["OrgName"] = "Organization name must be 3-255 characters";
            }

            // Phone (optional)
            if (!string.IsNullOrWhiteSpace(ph))
            {
                // digits only 9..12
                if (!Regex.IsMatch(ph, "^\\d{9,12}$"))
                {
                    errors["Phone"] = "Phone must be 9-12 digits";
                }
            }

            // Email (optional)
            if (!string.IsNullOrWhiteSpace(em))
            {
                if (!IsValidEmail(em))
                {
                    errors["Email"] = "Invalid email format";
                }
            }

            return errors;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                // MailAddress sẽ validate format cơ bản
                var addr = new MailAddress(email);
                return string.Equals(addr.Address, email, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}
