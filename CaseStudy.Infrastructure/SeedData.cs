using CaseStudy.Domain.VendorAggregate.ValueObjects;
using CaseStudy.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CaseStudy.Infrastructure;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var dbContext = new CaseStudyContext(serviceProvider.GetRequiredService<DbContextOptions<CaseStudyContext>>()
            , serviceProvider.GetRequiredService<ILogger<CaseStudyContext>>());

        if (!dbContext.Vendors.Any())
        {
            PopulateVendorTestData(dbContext);
        }

        if (!dbContext.Users.Any())
        {
            PopulateUserTestData(dbContext);
        }
    }

    private static void PopulateVendorTestData(CaseStudyContext dbContext)
    {
        foreach (var employee in dbContext.Vendors)
        {
            dbContext.Remove(employee);
        }

        dbContext.Vendors.Add(new Domain.VendorAggregate.Vendor()
        {
            Name = new Name("vendor1"),
            Address = new Address("v1_address1", "v1_address2", "Vienna", "Austria", "1234567890"),
            Mail = new Email("vendor1@example.com"),
            Notes = "This is just a note!",
            Phone = new Phone("(123) 456-7890"),
            Name2 = new Name("vendor1_2")
        });

        dbContext.Vendors.Add(new Domain.VendorAggregate.Vendor()
        {
            Name = new Name("vendor2"),
            Address = new Address("v2_address1", "v2_address2", "Hartberg", "Austria", "1234567999"),
            Mail = new Email("vendor2@example.com"),
            Notes = "This is just a note!",
            Phone = new Phone("(333) 456-7890"),
            Name2 = new Name("vendor2_2")
        });

        dbContext.Vendors.Add(new Domain.VendorAggregate.Vendor()
        {
            Name = new Name("vendor3"),
            Address = new Address("v3_address1", "v3_address2", "Graz", "Austria", "9876543210"),
            Mail = new Email("vendor3@example.com"),
            Notes = "This is just a note!",
            Phone = new Phone("(999) 456-7890"),
            Name2 = new Name("vendor3_2")
        });

        dbContext.SaveChanges();
    }

    private static void PopulateUserTestData(CaseStudyContext dbContext)
    {
        foreach (var user in dbContext.Users)
        {
            dbContext.Remove(user);
        }

        var passwordHasher = new PasswordHasher<ApplicationUser>();

        var appUser = new ApplicationUser()
        {
            Email = "admin@example.com",
            UserName = "admin",
        };

        appUser.NormalizedEmail = appUser.Email.ToUpper();
        appUser.NormalizedUserName = appUser.UserName.ToUpper();

        appUser.PasswordHash = passwordHasher.HashPassword(appUser, "P@ssW0rd");

        dbContext.Users.Add(appUser);

        dbContext.UserClaims.Add(new IdentityUserClaim<string>()
        {
            ClaimType = "Permission",
            ClaimValue = "Read",
            UserId = appUser.Id
        });
        dbContext.UserClaims.Add(new IdentityUserClaim<string>()
        {
            ClaimType = "Permission",
            ClaimValue = "Create",
            UserId = appUser.Id
        });
        dbContext.UserClaims.Add(new IdentityUserClaim<string>()
        {
            ClaimType = "Permission",
            ClaimValue = "Update",
            UserId = appUser.Id
        });
        dbContext.UserClaims.Add(new IdentityUserClaim<string>()
        {
            ClaimType = "Permission",
            ClaimValue = "Delete",
            UserId = appUser.Id
        });

        dbContext.SaveChanges();
    }
}