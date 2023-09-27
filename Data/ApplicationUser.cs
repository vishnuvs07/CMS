using Microsoft.AspNetCore.Identity;

namespace CMS.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
