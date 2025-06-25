

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learnava.DataAccess.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        [NotMapped]
        public string Role { get; set; } = string.Empty;
    }
}
