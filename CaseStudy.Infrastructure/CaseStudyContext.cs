using CaseStudy.Domain.VendorAggregate;
using CaseStudy.Domain.VendorAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CaseStudy.Infrastructure;

public class CaseStudyContext : IdentityDbContextBase
{

    public CaseStudyContext(DbContextOptions<CaseStudyContext> options, ILogger<CaseStudyContext> logger) : base(options, logger)
    {
    }

    public virtual DbSet<BankAccount> BankAccounts { get; set; }
    public virtual DbSet<ContactPerson> ContactPeople { get; set; }
    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

}
