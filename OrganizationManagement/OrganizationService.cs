namespace OrganizationManagement
{
    public class OrganizationService
    {
        private readonly OrganizationRepository _repo;

        public OrganizationService(OrganizationRepository repo)
        {
            _repo = repo;
        }

        public OrganizationService() : this(new OrganizationRepository()) { }

        public SaveOrgResult Save(string orgName, string address, string phone, string email)
        {
            var result = new SaveOrgResult();

            // Normalize
            orgName = (orgName ?? string.Empty).Trim();
            address = string.IsNullOrWhiteSpace(address) ? null : address.Trim();
            phone = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim();
            email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();

            var errors = OrganizationValidator.Validate(orgName, address, phone, email);
            if (errors.Count > 0)
            {
                result.Success = false;
                result.Message = "Validation failed";
                result.FieldErrors = errors;
                return result;
            }

            if (_repo.ExistsName(orgName))
            {
                result.Success = false;
                result.Message = "Organization Name already exists";
                return result;
            }

            var org = new Organization
            {
                OrgName = orgName,
                Address = address,
                Phone = phone,
                Email = email
            };

            org.Id = _repo.Insert(org);

            result.Success = true;
            result.Message = "Save successfully";
            result.SavedOrganization = org;
            return result;
        }
    }
}
