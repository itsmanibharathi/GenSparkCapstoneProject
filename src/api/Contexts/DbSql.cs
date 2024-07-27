using api.Models;
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
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyMediaFile> MediaFiles { get; set; }
        public DbSet<PropertyLand> Lands { get; set; }
        public DbSet<PropertyHome> Homes { get; set; }
        public DbSet<PropertyAmenity> Amenities { get; set; }

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
            #endregion

            #region UserAuth

            modelBuilder.Entity<UserAuth>().ToTable("UserAuths");
            modelBuilder.Entity<UserAuth>()
                .HasKey(x => x.UserId);
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



        }
        #endregion
    }
}
