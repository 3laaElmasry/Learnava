
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Learnava.DataAccess.Data
{
    public class ApplicationDbConetxt : IdentityDbContext
    {
        public ApplicationDbConetxt(DbContextOptions<ApplicationDbConetxt> options) : base(options) 
        {
            
        }


    }
}
