using api.Models;
using api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace api.Contexts
{
    public class DbSql : DbContext
    {
        public DbSql(DbContextOptions<DbSql> options) : base(options)
        {
        }

        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<UserAuth> UserAuths { get; set; }
        public DbSet<UserVerify> UserVerifies { get; set; }
        public DbSet<UserSubscriptionPlan> UserSubscriptionPlans { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyMediaFile> MediaFiles { get; set; }
        public DbSet<PropertyLand> Lands { get; set; }
        public DbSet<PropertyHome> Homes { get; set; }
        public DbSet<PropertyAmenity> Amenities { get; set; }
        public DbSet<UserPropertyInteraction> UserPropertyInteractions { get; set; }


        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region User
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<User>().HasKey(x => x.UserId);
            modelBuilder.Entity<User>()
                .HasOne(x => x.UserAuth)
                .WithOne(x => x.User)
                .HasForeignKey<UserAuth>(x => x.UserId);
            modelBuilder.Entity<User>()
                .HasMany(x => x.Property)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<User>()
                .HasOne(x => x.UserVerify)
                .WithOne(x => x.User)
                .HasForeignKey<UserVerify>(x => x.UserId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.UserSubscriptionPlan)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<User>()
                .HasMany(x => x.UserPropertyInteractions)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region UserAuth

            modelBuilder.Entity<UserAuth>().ToTable("UserAuths");
            modelBuilder.Entity<UserAuth>()
                .HasKey(x => x.UserId);
            #endregion

            #region UserVerify
            modelBuilder.Entity<UserVerify>().ToTable("UserVerifies");
            modelBuilder.Entity<UserVerify>()
                .HasKey(x => x.Id);
            #endregion

            #region UserSubscriptionPlan
            modelBuilder.Entity<UserSubscriptionPlan>().ToTable("UserSubscriptionPlans");
            modelBuilder.Entity<UserSubscriptionPlan>()
                .HasKey(x => x.UserSubscriptionPlanId);
            #endregion

            #region SubscriptionPlan
            modelBuilder.Entity<SubscriptionPlan>().ToTable("SubscriptionPlans");
            modelBuilder.Entity<SubscriptionPlan>()
                .HasKey(x => x.SubscriptionPlanId);
            modelBuilder.Entity<SubscriptionPlan>()
                .HasOne(x => x.UserSubscriptionPlan)
                .WithOne(x => x.SubscriptionPlan)
                .HasForeignKey<UserSubscriptionPlan>(x => x.SubscriptionPlanId);

            modelBuilder.Entity<SubscriptionPlan>()
                .Property(x => x.SubscriptionPlanPrice)
                .HasColumnType("decimal(18,2)");

            #region SubscriptionPlan Seed Data
            modelBuilder.Entity<SubscriptionPlan>().HasData(
                               new SubscriptionPlan
                               {
                                   SubscriptionPlanId = 101,
                                   SubscriptionPlanName = "Free Trial On Contact Me",
                                   SubscriptionPlanDescription = "New User View Contact Subscription Plan",
                                   SubscriptionPlanPrice = 0,
                                   SubscriptionPlanDuration = 3,
                                   SubscriptionPlanDurationType = SubscriptionPlanDurationType.Days,
                                   IsActive = true,
                                   CreatedAt = System.DateTime.Now,
                               },
                               new SubscriptionPlan
                               {
                                   SubscriptionPlanId = 102,
                                   SubscriptionPlanName = "Free Trial On View Owner info",
                                   SubscriptionPlanDescription = "View Owner info",
                                   SubscriptionPlanPrice = 0,
                                   SubscriptionPlanDuration = 2,
                                   SubscriptionPlanDurationType = SubscriptionPlanDurationType.Count,
                                   IsActive = true,
                                   CreatedAt = System.DateTime.Now,
                               },
                               new SubscriptionPlan
                               {
                                   SubscriptionPlanId = 103,
                                   SubscriptionPlanName = "Contact Me",
                                   SubscriptionPlanDescription = "Share your contact info to the Owner",
                                   SubscriptionPlanPrice = 100,
                                   SubscriptionPlanDuration = 30,
                                   SubscriptionPlanDurationType = SubscriptionPlanDurationType.Days,
                                   IsActive = true,
                                   CreatedAt = System.DateTime.Now,
                               },
                               new SubscriptionPlan
                               {
                                   SubscriptionPlanId = 104,
                                   SubscriptionPlanName = "View Owner info",
                                   SubscriptionPlanDescription = "View Owner info for 10 Property",
                                   SubscriptionPlanPrice = 100,
                                   SubscriptionPlanDuration = 10,
                                   SubscriptionPlanDurationType = SubscriptionPlanDurationType.Count,
                                   IsActive = true,
                                   CreatedAt = System.DateTime.Now,
                               }
                               );

            #endregion
            #endregion

            #region property
            modelBuilder.Entity<Property>().ToTable("Properties");

            modelBuilder.Entity<Property>()
                .HasKey(x => x.PropertyId);
            modelBuilder.Entity<Property>()
                .HasMany(x => x.Amenities)
                .WithOne(x => x.Property)
                .HasForeignKey(x => x.PropertyId);
            modelBuilder.Entity<Property>()
                .HasMany(x => x.MediaFiles)
                .WithOne(x => x.Property)
                .HasForeignKey(x => x.PropertyId);
            modelBuilder.Entity<Property>()
                .HasOne(x => x.Home)
                .WithOne(x => x.Property)
                .HasForeignKey<PropertyHome>(x => x.PropertyId);
            modelBuilder.Entity<Property>()
                .HasOne(x => x.Land)
                .WithOne(x => x.Property)
                .HasForeignKey<PropertyLand>(x => x.PropertyId);
            modelBuilder.Entity<Property>()
                .HasMany(x => x.UserPropertyInteractions)
                .WithOne(x => x.Property)
                .HasForeignKey(x => x.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);
            #region Property Attributes
            modelBuilder.Entity<Property>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18,2)");
            #endregion

            #endregion

            #region MediaFile
            modelBuilder.Entity<PropertyMediaFile>().ToTable("MediaFiles");
            modelBuilder.Entity<PropertyMediaFile>()
                .HasKey(x => x.MediaFileId);
            #endregion

            #region Land
            modelBuilder.Entity<PropertyLand>().ToTable("Lands");
            modelBuilder.Entity<PropertyLand>()
                .HasKey(x => x.PropertyId);

            #endregion

            #region Home
            modelBuilder.Entity<PropertyHome>().ToTable("Homes");
            modelBuilder.Entity<PropertyHome>()
                .HasKey(x => x.PropertyId);
            #endregion

            #region Amenity
            modelBuilder.Entity<PropertyAmenity>().ToTable("Amenities");
            modelBuilder.Entity<PropertyAmenity>()
                .HasKey(x => x.AmenityId);
            #endregion

            #region UserPropertyInteraction
            modelBuilder.Entity<UserPropertyInteraction>().ToTable("UserPropertyInteractions");
            modelBuilder.Entity<UserPropertyInteraction>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<UserPropertyInteraction>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserPropertyInteractions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserPropertyInteraction>()
                .HasOne(x => x.Property)
                .WithMany(x => x.UserPropertyInteractions)
                .HasForeignKey(x => x.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion


        }
        #endregion
    }
}
