using CaseStudy.Domain.VendorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseStudy.Infrastructure.MappingConfigurations;

public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder.ToTable("Vendor");

        builder.Property(f => f.Id).ValueGeneratedOnAdd();

        builder.OwnsOne(e => e.Address, ba =>
        {
            ba.Property(t => t.Address1).HasColumnName("Address1").HasMaxLength(100);
        });
        builder.OwnsOne(e => e.Address, ba =>
        {
            ba.Property(t => t.Address2).HasColumnName("Address2").HasMaxLength(100);
        });
        builder.OwnsOne(e => e.Address, ba =>
        {
            ba.Property(t => t.City).HasColumnName("City").HasMaxLength(100);
        });
        builder.OwnsOne(e => e.Address, ba =>
        {
            ba.Property(t => t.Country).HasColumnName("Country").HasMaxLength(100);
        });
        builder.OwnsOne(e => e.Address, ba =>
        {
            ba.Property(t => t.Zip).HasColumnName("Zip").HasMaxLength(100);
        });

        builder.OwnsOne(e => e.Phone, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("Phone").HasMaxLength(100);
        });

        builder.OwnsOne(e => e.Name, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("Name").HasMaxLength(100);
        });
        builder.OwnsOne(e => e.Name2, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("Name2").HasMaxLength(100);
        });

        builder.OwnsOne(e => e.Mail, ba =>
        {
            ba.Property(t => t.Value).HasColumnName("Mail").HasMaxLength(100);
        });

        builder.Property(e => e.Notes).HasMaxLength(255);
    }
}