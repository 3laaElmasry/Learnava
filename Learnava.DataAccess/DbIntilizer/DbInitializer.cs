

using Learnava.DataAccess.Data.Entities;
using Learnava.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Learnava.DataAccess.DbIntilizer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext db,
            ILogger<DbInitializer> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
            _logger = logger;
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

                // Seed Instructor
                var instructorUser1 = new ApplicationUser
                {
                    UserName = "ins1@learnava.com",
                    Email = "ins1@learnava.com",
                    FullName = "Apo-Hadhoud",
                    PhoneNumber = "+201111122222",
                    Role = SD.Role_Instructor,
                    EmailConfirmed = true
                };
                if (await _userManager.FindByEmailAsync(instructorUser1.Email) == null)
                {
                    var result = await _userManager.CreateAsync(instructorUser1, "ins123");
                    if (result.Succeeded)
                        await _userManager.AddToRoleAsync(instructorUser1, SD.Role_Instructor);
                }


                // Seed Instructor
                var instructorUser2 = new ApplicationUser
                {
                    UserName = "ins2@learnava.com",
                    Email = "ins2@learnava.com",
                    FullName = "Adel-Nasim",
                    PhoneNumber = "+201111111111",
                    Role = SD.Role_Instructor,
                    EmailConfirmed = true
                };
                if (await _userManager.FindByEmailAsync(instructorUser2.Email) == null)
                {
                    var result = await _userManager.CreateAsync(instructorUser2, "ins123");
                    if (result.Succeeded)
                        await _userManager.AddToRoleAsync(instructorUser2, SD.Role_Instructor);
                }
                // Seed Student
                var studentUser = new ApplicationUser
                {
                    UserName = "student@learnava.com",
                    Email = "student@learnava.com",
                    FullName = "Student One",
                    PhoneNumber = "+201222222222",
                    Role = SD.Role_Student,
                    EmailConfirmed = true
                };
                if (await _userManager.FindByEmailAsync(studentUser.Email) == null)
                {
                    var result = await _userManager.CreateAsync(studentUser, "student123");
                    if (result.Succeeded)
                        await _userManager.AddToRoleAsync(studentUser, SD.Role_Student);
                }
                else
                {
                    _logger.LogInformation("Roles already exist; skipping role and admin user creation.");
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
