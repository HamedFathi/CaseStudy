using CaseStudy.Domain.VendorAggregate.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy.Infrastructure.MappingConfigurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.ToTable("BankAccount");

        builder.Property(f => f.Id).ValueGeneratedOnAdd();

        builder.OwnsOne(e => e.Name, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("Name").HasMaxLength(100);
        });

        builder.OwnsOne(e => e.IBAN, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("IBAN").HasMaxLength(100);
        });

        builder.OwnsOne(e => e.BIC, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("BIC").HasMaxLength(100);
        });

        builder.HasOne(d => d.Vendor).WithMany(p => p.BankAccounts)
            .HasForeignKey(d => d.Id)
            .HasConstraintName("FK_BankAccounts_Vendors");
    }
}