using CaseStudy.Domain.VendorAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseStudy.Infrastructure.MappingConfigurations;

public class ContactPersonConfiguration : IEntityTypeConfiguration<ContactPerson>
{
    public void Configure(EntityTypeBuilder<ContactPerson> builder)
    {

        builder.ToTable("ContactPerson");

        builder.Property(f => f.Id).ValueGeneratedOnAdd();

        builder.OwnsOne(e => e.FirstName, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("FirstName").HasMaxLength(100);
        });
        builder.OwnsOne(e => e.LastName, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("LastName").HasMaxLength(100);
        });

        builder.OwnsOne(e => e.Email, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("Email").HasMaxLength(100);
        });

        builder.OwnsOne(e => e.Phone, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("Phone").HasMaxLength(100);
        });

        builder.HasOne(d => d.Vendor).WithMany(p => p.ContactPeople)
            .HasForeignKey(d => d.Id)
            .HasConstraintName("FK_ContactPeople_Vendors");
    }
}