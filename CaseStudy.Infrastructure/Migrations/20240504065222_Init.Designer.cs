﻿// <auto-generated />
using System;
using CaseStudy.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CaseStudy.Infrastructure.Migrations
{
    [DbContext(typeof(CaseStudyContext))]
    [Migration("20240504065222_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("CaseStudy.Domain.VendorAggregate.Entities.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("BankAccount", (string)null);
                });

            modelBuilder.Entity("CaseStudy.Domain.VendorAggregate.Entities.ContactPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ContactPerson", (string)null);
                });

            modelBuilder.Entity("CaseStudy.Domain.VendorAggregate.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Notes")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Vendor", (string)null);
                });

            modelBuilder.Entity("CaseStudy.Infrastructure.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CaseStudy.Domain.VendorAggregate.Entities.BankAccount", b =>
                {
                    b.HasOne("CaseStudy.Domain.VendorAggregate.Vendor", "Vendor")
                        .WithMany("BankAccounts")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BankAccounts_Vendors");

                    b.OwnsOne("CaseStudy.Domain.VendorAggregate.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<int>("BankAccountId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Name");

                            b1.HasKey("BankAccountId");

                            b1.ToTable("BankAccount");

                            b1.WithOwner()
                                .HasForeignKey("BankAccountId");
                        });

                    b.OwnsOne("CaseStudy.Domain.VendorAggregate.ValueObjects.BIC", "BIC", b1 =>
                        {
                            b1.Property<int>("BankAccountId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("BIC");

                            b1.HasKey("BankAccountId");

                            b1.ToTable("BankAccount");

                            b1.WithOwner()
                                .HasForeignKey("BankAccountId");
                        });

                    b.OwnsOne("CaseStudy.Domain.VendorAggregate.ValueObjects.IBAN", "IBAN", b1 =>
                        {
                            b1.Property<int>("BankAccountId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("IBAN");

                            b1.HasKey("BankAccountId");

                            b1.ToTable("BankAccount");

                            b1.WithOwner()
                                .HasForeignKey("BankAccountId");
                        });

                    b.Navigation("BIC")
                        .IsRequired();

                    b.Navigation("IBAN")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("CaseStudy.Domain.VendorAggregate.Entities.ContactPerson", b =>
                {
                    b.HasOne("CaseStudy.Domain.VendorAggregate.Vendor", "Vendor")
                        .WithMany("ContactPeople")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ContactPeople_Vendors");

                    b.OwnsOne("CaseStudy.Domain.VendorAggregate.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<int>("ContactPersonId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Email");

                            b1.HasKey("ContactPersonId");

                            b1.ToTable("ContactPerson");

                            b1.WithOwner()
                                .HasForeignKey("ContactPersonId");
                        });

                    b.OwnsOne("CaseStudy.Domain.VendorAggregate.ValueObjects.Name", "FirstName", b1 =>
                        {
                            b1.Property<int>("ContactPersonId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("FirstName");

                            b1.HasKey("ContactPersonId");

                            b1.ToTable("ContactPerson");

                            b1.WithOwner()
                                .HasForeignKey("ContactPersonId");
                        });

                    b.OwnsOne("CaseStudy.Domain.VendorAggregate.ValueObjects.Name", "LastName", b1 =>
                        {
                            b1.Property<int>("ContactPersonId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("LastName");

                            b1.HasKey("ContactPersonId");

                            b1.ToTable("ContactPerson");

                            b1.WithOwner()
                                .HasForeignKey("ContactPersonId");
                        });

                    b.OwnsOne("CaseStudy.Domain.VendorAggregate.ValueObjects.Phone", "Phone", b1 =>
                        {
                            b1.Property<int>("ContactPersonId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Phone");

                            b1.HasKey("ContactPersonId");

                            b1.ToTable("ContactPerson");

                            b1.WithOwner()
                                .HasForeignKey("ContactPersonId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("FirstName")
                        .IsRequired();

                    b.Navigation("LastName")
                        .IsRequired();

                    b.Navigation("Phone")
                        .IsRequired();

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("CaseStudy.Domain.VendorAggregate.Vendor", b =>
                {
                    b.OwnsOne("CaseStudy.Domain.VendorAggregate.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<int>("VendorId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Address1")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Address1");

                            b1.Property<string>("Address2")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Address2");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Country");

                            b1.Property<string>("Zip")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Zip");

                            b1.HasKey("VendorId");

                            b1.ToTable("Vendor");

                            b1.WithOwner()
                                .HasForeignKey("VendorId");
                        });

                    b.OwnsOne("CaseStudy.Domain.VendorAggregate.ValueObjects.Email", "Mail", b1 =>
                        {
                            b1.Property<int>("VendorId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Mail");

                            b1.HasKey("VendorId");

                            b1.ToTable("Vendor");

                            b1.WithOwner()
                                .HasForeignKey("VendorId");
                        });

                    b.OwnsOne("CaseStudy.Domain.VendorAggregate.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<int>("VendorId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Name");

                            b1.HasKey("VendorId");

                            b1.ToTable("Vendor");

                            b1.WithOwner()
                                .HasForeignKey("VendorId");
                        });

                    b.OwnsOne("CaseStudy.Domain.VendorAggregate.ValueObjects.Name", "Name2", b1 =>
                        {
                            b1.Property<int>("VendorId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Name2");

                            b1.HasKey("VendorId");

                            b1.ToTable("Vendor");

                            b1.WithOwner()
                                .HasForeignKey("VendorId");
                        });

                    b.OwnsOne("CaseStudy.Domain.VendorAggregate.ValueObjects.Phone", "Phone", b1 =>
                        {
                            b1.Property<int>("VendorId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT")
                                .HasColumnName("Phone");

                            b1.HasKey("VendorId");

                            b1.ToTable("Vendor");

                            b1.WithOwner()
                                .HasForeignKey("VendorId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Mail")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Name2");

                    b.Navigation("Phone")
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CaseStudy.Infrastructure.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CaseStudy.Infrastructure.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CaseStudy.Infrastructure.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CaseStudy.Infrastructure.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CaseStudy.Domain.VendorAggregate.Vendor", b =>
                {
                    b.Navigation("BankAccounts");

                    b.Navigation("ContactPeople");
                });
#pragma warning restore 612, 618
        }
    }
}
