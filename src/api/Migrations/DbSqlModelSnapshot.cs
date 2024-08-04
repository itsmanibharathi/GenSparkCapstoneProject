﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Contexts;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(DbSql))]
    partial class DbSqlModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("api.Models.Property", b =>
                {
                    b.Property<int>("PropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyId"), 1L, 1);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Landmark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PropertyId");

                    b.HasIndex("UserId");

                    b.ToTable("Properties", (string)null);
                });

            modelBuilder.Entity("api.Models.PropertyAmenity", b =>
                {
                    b.Property<int>("AmenityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AmenityId"), 1L, 1);

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("AmenityId");

                    b.HasIndex("PropertyId");

                    b.ToTable("Amenities", (string)null);
                });

            modelBuilder.Entity("api.Models.PropertyHome", b =>
                {
                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("FloorNumber")
                        .HasColumnType("int");

                    b.Property<int>("FurnishingStatus")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfBathrooms")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfBedrooms")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("YearBuilt")
                        .HasColumnType("int");

                    b.HasKey("PropertyId");

                    b.ToTable("Homes", (string)null);
                });

            modelBuilder.Entity("api.Models.PropertyLand", b =>
                {
                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("LandArea")
                        .HasColumnType("float");

                    b.Property<int>("LandType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ZoningInformation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PropertyId");

                    b.ToTable("Lands", (string)null);
                });

            modelBuilder.Entity("api.Models.PropertyMediaFile", b =>
                {
                    b.Property<int>("MediaFileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MediaFileId"), 1L, 1);

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MediaFileId");

                    b.HasIndex("PropertyId");

                    b.ToTable("MediaFiles", (string)null);
                });

            modelBuilder.Entity("api.Models.SubscriptionPlan", b =>
                {
                    b.Property<int>("SubscriptionPlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscriptionPlanId"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("SubscriptionPlanDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubscriptionPlanDuration")
                        .HasColumnType("int");

                    b.Property<int>("SubscriptionPlanDurationType")
                        .HasColumnType("int");

                    b.Property<string>("SubscriptionPlanName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SubscriptionPlanPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("SubscriptionPlanId");

                    b.ToTable("SubscriptionPlans", (string)null);

                    b.HasData(
                        new
                        {
                            SubscriptionPlanId = 101,
                            CreatedAt = new DateTime(2024, 8, 4, 20, 2, 47, 476, DateTimeKind.Local).AddTicks(9028),
                            IsActive = true,
                            SubscriptionPlanDescription = "New User View Contact Subscription Plan",
                            SubscriptionPlanDuration = 3,
                            SubscriptionPlanDurationType = 1,
                            SubscriptionPlanName = "Free Trial On Contact Me",
                            SubscriptionPlanPrice = 0m
                        },
                        new
                        {
                            SubscriptionPlanId = 102,
                            CreatedAt = new DateTime(2024, 8, 4, 20, 2, 47, 476, DateTimeKind.Local).AddTicks(9044),
                            IsActive = true,
                            SubscriptionPlanDescription = "View Owner info",
                            SubscriptionPlanDuration = 2,
                            SubscriptionPlanDurationType = 0,
                            SubscriptionPlanName = "Free Trial On View Owner info",
                            SubscriptionPlanPrice = 0m
                        },
                        new
                        {
                            SubscriptionPlanId = 103,
                            CreatedAt = new DateTime(2024, 8, 4, 20, 2, 47, 476, DateTimeKind.Local).AddTicks(9047),
                            IsActive = true,
                            SubscriptionPlanDescription = "Share your contact info to the Owner",
                            SubscriptionPlanDuration = 30,
                            SubscriptionPlanDurationType = 1,
                            SubscriptionPlanName = "Contact Me",
                            SubscriptionPlanPrice = 100m
                        },
                        new
                        {
                            SubscriptionPlanId = 104,
                            CreatedAt = new DateTime(2024, 8, 4, 20, 2, 47, 476, DateTimeKind.Local).AddTicks(9048),
                            IsActive = true,
                            SubscriptionPlanDescription = "View Owner info for 10 Property",
                            SubscriptionPlanDuration = 10,
                            SubscriptionPlanDurationType = 0,
                            SubscriptionPlanName = "View Owner info",
                            SubscriptionPlanPrice = 100m
                        });
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsOwner")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("TeneantVerificationCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserProfileImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("api.Models.UserAuth", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("UserAuths", (string)null);
                });

            modelBuilder.Entity("api.Models.UserPropertyInteraction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("InteractionType")
                        .HasColumnType("int");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPropertyInteractions", (string)null);
                });

            modelBuilder.Entity("api.Models.UserSubscriptionPlan", b =>
                {
                    b.Property<int>("UserSubscriptionPlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserSubscriptionPlanId"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("SubscriptionEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubscriptionPlanId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubscriptionStartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserSubscriptionPlanId");

                    b.HasIndex("SubscriptionPlanId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("UserSubscriptionPlans", (string)null);
                });

            modelBuilder.Entity("api.Models.UserVerify", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("UserVerifyStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserVerifies", (string)null);
                });

            modelBuilder.Entity("api.Models.Property", b =>
                {
                    b.HasOne("api.Models.User", "User")
                        .WithMany("Property")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("api.Models.PropertyAmenity", b =>
                {
                    b.HasOne("api.Models.Property", "Property")
                        .WithMany("Amenities")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("api.Models.PropertyHome", b =>
                {
                    b.HasOne("api.Models.Property", "Property")
                        .WithOne("Home")
                        .HasForeignKey("api.Models.PropertyHome", "PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("api.Models.PropertyLand", b =>
                {
                    b.HasOne("api.Models.Property", "Property")
                        .WithOne("Land")
                        .HasForeignKey("api.Models.PropertyLand", "PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("api.Models.PropertyMediaFile", b =>
                {
                    b.HasOne("api.Models.Property", "Property")
                        .WithMany("MediaFiles")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("api.Models.UserAuth", b =>
                {
                    b.HasOne("api.Models.User", "User")
                        .WithOne("UserAuth")
                        .HasForeignKey("api.Models.UserAuth", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("api.Models.UserPropertyInteraction", b =>
                {
                    b.HasOne("api.Models.Property", "Property")
                        .WithMany("UserPropertyInteractions")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.User", "User")
                        .WithMany("UserPropertyInteractions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Property");

                    b.Navigation("User");
                });

            modelBuilder.Entity("api.Models.UserSubscriptionPlan", b =>
                {
                    b.HasOne("api.Models.SubscriptionPlan", "SubscriptionPlan")
                        .WithOne("UserSubscriptionPlan")
                        .HasForeignKey("api.Models.UserSubscriptionPlan", "SubscriptionPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.User", "User")
                        .WithMany("UserSubscriptionPlan")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubscriptionPlan");

                    b.Navigation("User");
                });

            modelBuilder.Entity("api.Models.UserVerify", b =>
                {
                    b.HasOne("api.Models.User", "User")
                        .WithOne("UserVerify")
                        .HasForeignKey("api.Models.UserVerify", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("api.Models.Property", b =>
                {
                    b.Navigation("Amenities");

                    b.Navigation("Home");

                    b.Navigation("Land");

                    b.Navigation("MediaFiles");

                    b.Navigation("UserPropertyInteractions");
                });

            modelBuilder.Entity("api.Models.SubscriptionPlan", b =>
                {
                    b.Navigation("UserSubscriptionPlan")
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.Navigation("Property");

                    b.Navigation("UserAuth")
                        .IsRequired();

                    b.Navigation("UserPropertyInteractions");

                    b.Navigation("UserSubscriptionPlan");

                    b.Navigation("UserVerify");
                });
#pragma warning restore 612, 618
        }
    }
}
