using System.Collections.Generic;

namespace OrganizationManagement
{
    public static class OrganizationValidator
    {
        public static Dictionary<string, string> Validate(string orgName, string address, string phone, string email)
        {
            var errors = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(orgName))
                errors["OrgName"] = "Organization name is required";

            // mày muốn validate thêm phone/email thì thêm ở đây.

            return errors;
        }
    }
}
