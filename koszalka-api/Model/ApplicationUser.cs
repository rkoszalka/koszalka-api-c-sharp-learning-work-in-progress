using Microsoft.AspNetCore.Identity;

namespace koszalka_api.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
