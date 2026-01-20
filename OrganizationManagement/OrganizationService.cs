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

            _repo.Insert(new Organization
            {
                OrgName = orgName,
                Address = address,
                Phone = phone,
                Email = email
            });

            result.Success = true;
            result.Message = "Save successfully";
            return result;
        }
    }
}
