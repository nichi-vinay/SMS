using Microsoft.AspNetCore.Identity;

namespace sms.api.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
