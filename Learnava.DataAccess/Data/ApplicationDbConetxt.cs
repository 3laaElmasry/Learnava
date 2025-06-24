
using Learnava.DataAccess.Data.Configurations;
using Learnava.DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Learnava.DataAccess.Data
{
    public class ApplicationDbConetxt : IdentityDbContext
    {
        public ApplicationDbConetxt(DbContextOptions<ApplicationDbConetxt> options) : base(options) 
        {
            
        }

        public DbSet<Instructor> instructors { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Enrollment> enrollments { get; set; }  

        public DbSet<Student> students { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(StudentConfigurations).Assembly);

        }
    }
}
