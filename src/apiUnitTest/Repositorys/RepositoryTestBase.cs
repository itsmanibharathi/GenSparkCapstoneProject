using api.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiUnitTest.Repositorys
{
    public class RepositoryTestBase
    {
        public DbSql _context;

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbSql>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new DbSql(optionsBuilder.Options);
        }

        
        public async Task SeedDataUser()
        {
            await _context.Users.AddRangeAsync(SeedData.Users);
            await _context.SaveChangesAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await _context.Database.EnsureDeletedAsync();
        }
        public void DummyDB()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbSql>()
                .UseSqlServer("InvalidConnectionString");
            _context = new DbSql(optionsBuilder.Options);
        }

        public void EmptyDB()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbSql>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new DbSql(optionsBuilder.Options);
        }
    }
}
