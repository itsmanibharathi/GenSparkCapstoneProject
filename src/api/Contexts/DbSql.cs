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
            #endregion

            #region UserAuth

            modelBuilder.Entity<UserAuth>().ToTable("UserAuths");
            modelBuilder.Entity<UserAuth>()
                .HasKey(x => x.UserId);
            #endregion
        }
        #endregion
    }
}
