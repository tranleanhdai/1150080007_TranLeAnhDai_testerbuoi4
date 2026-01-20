using System.Collections.Generic;

namespace OrganizationManagement
{
    public class SaveOrgResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> FieldErrors { get; set; } = new Dictionary<string, string>();
    }
}
