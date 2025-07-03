

using Learnava.DataAccess.Data.Entities;
using Learnava.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace Learnava.DataAccess.DbIntilizer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly ILogger<DbInitializer> _logger;
        private readonly ApplicationDbContext _context;

        public DbInitializer(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db,
            ILogger<DbInitializer> logger,
            ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
            _logger = logger;
            _context = context;
        }

        public async Task InitializeAsync()
        {
            // Apply migrations if pending
            try
            {
                var pendingMigrations = await _db.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    _logger.LogInformation("Applying pending migrations: {Migrations}", string.Join(", ", pendingMigrations));
                    await _db.Database.MigrateAsync();
                    _logger.LogInformation("Migrations applied successfully.");
                }
                else
                {
                    _logger.LogInformation("No pending migrations.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to apply migrations.");
                throw; // Rethrow to halt startup and allow debugging
            }

            // Create roles if they don't exist
            try
            {
                // Create roles if they don't exist
                string[] roles = { SD.Role_Admin, SD.Role_Instructor, SD.Role_Student };
                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Seed Admin
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@learnava.com",
                    Email = "admin@learnava.com",
                    FullName = "Admin User",
                    PhoneNumber = "+201000000000",
                    Role = SD.Role_Admin,
                    EmailConfirmed = true
                };
                if (await _userManager.FindByEmailAsync(adminUser.Email) == null)
                {
                    var result = await _userManager.CreateAsync(adminUser, "admin123");
                    if (result.Succeeded)
                        await _userManager.AddToRoleAsync(adminUser, SD.Role_Admin);
                }

                // Seed Instructor
                var instructorUser = new ApplicationUser
                {
                    UserName = "ins@learnava.com",
                    Email = "ins@learnava.com",
                    FullName = "Moshrif",
                    PhoneNumber = "+201111111133",
                    Role = SD.Role_Instructor,
                    EmailConfirmed = true
                };
                if (await _userManager.FindByEmailAsync(instructorUser.Email) == null)
                {
                    var result = await _userManager.CreateAsync(instructorUser, "ins123");
                    if (result.Succeeded)
                        await _userManager.AddToRoleAsync(instructorUser, SD.Role_Instructor);
                }

               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize roles or admin user.");
                throw; // Rethrow to halt startup
            }

        }
    }
}
