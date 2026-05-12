using MedMateAI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MedMateAI.Infrastructure.Persistence.Seeder;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var sp = scope.ServiceProvider;

        var db = sp.GetRequiredService<ApplicationDbContext>();
        
        await db.Database.MigrateAsync(cancellationToken);

        var roleManager = sp.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();
      

        var roles = new[] { "Admin", "Staff", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }

        var adminEmail = "admin@medmate.local";
        var adminPassword = "Admin@12345";
        var adminDisplayName = "System Admin";

        var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
        if (existingAdmin is null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                DisplayName = adminDisplayName,
            };

            var createResult = await userManager.CreateAsync(adminUser, adminPassword);
            if (createResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        var staffEmail = "staff@medmate.local";
        var staffPassword = "Staff@12345";
        var staffDisplayName = "Demo Staff";

        var existingStaff = await userManager.FindByEmailAsync(staffEmail);
        if (existingStaff is null)
        {
            var staffUser = new ApplicationUser
            {
                UserName = staffEmail,
                Email = staffEmail,
                EmailConfirmed = true,
                DisplayName = staffDisplayName,
            };

            var createResult = await userManager.CreateAsync(staffUser, staffPassword);
            if (createResult.Succeeded)
            {
                await userManager.AddToRoleAsync(staffUser, "Staff");
            }
        }
    }
}

