using System.Collections.Generic;

namespace OrganizationManagement
{
    public class SaveOrgResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> FieldErrors { get; set; } = new Dictionary<string, string>();

        // phục vụ UI: sau khi Save thành công, trả lại Organization (có Id)
        public Organization SavedOrganization { get; set; }
    }
}
