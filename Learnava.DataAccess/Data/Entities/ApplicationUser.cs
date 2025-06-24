

using Microsoft.AspNetCore.Identity;

namespace Learnava.DataAccess.Data.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
