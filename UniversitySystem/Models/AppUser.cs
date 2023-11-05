using Microsoft.AspNetCore.Identity;

namespace UniversitySystem.Models
{
    public class AppUser : IdentityUser
    {

        public string Department { get; set; } = string.Empty;
    }
}
