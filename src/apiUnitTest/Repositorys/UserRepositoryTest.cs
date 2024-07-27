using api.Exceptions;
using api.Models;
using api.Repositories;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiUnitTest.Repositorys
{
    internal class UserRepositoryTest : RepositoryTestBase
    {
        private UserRepository _UserRepository;

        [SetUp]
        public async Task Setup()
        {
            _UserRepository = new UserRepository(_context);
            await SeedDataUser();
        }

        [Test]
        public async Task AddUser()
        {
            User User = new User()
            {
                UserName = "Ludena",
                UserEmail = "ludena@gmail.com",
                UserPhoneNumber = "1234567890",
            };
            var result = await _UserRepository.AddAsync(User);
            Assert.IsTrue(result.UserId == 3);
        }

        [Test]
        public async Task AddDuplicateUser()
        {
            User User = new User()
            {
                UserName = "Mani",
                UserEmail = "mani@gmail.com",
                UserPhoneNumber = "1234567890"
            };

            try
            {
                await _UserRepository.AddAsync(User);
            }
            catch (EntityAlreadyExistsException<User> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass(ex.Message);
            }
        }


        [Test]
        public async Task GetUser()
        {
            var result = await _UserRepository.GetAsync(1);
            Assert.IsTrue(result.UserId == 1);
        }
        [Test]
        public async Task GetUserNotFoundException()
        {
            try
            {
                var result = await _UserRepository.GetAsync(3);
            }
            catch (EntityNotFoundException<User> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }
        
        [Test]
        public async Task GetAllUser()
        {
            var result = await _UserRepository.GetAsync();
            Assert.IsTrue(result.Count() == 2);
        }

        

        [Test]
        public async Task UpdateUser()
        {
            var User = await _UserRepository.GetAsync(1);
            User.UserName = "Mani M";
            var result = await _UserRepository.UpdateAsync(User);
            Assert.IsTrue(result.UserName == "Mani M");
        }

        [Test]
        public async Task UpdateUserNotFountException()
        {
            try
            {
                var User = await _UserRepository.GetAsync(1);
                User.UserName = "Mani M";
                var result = await _UserRepository.UpdateAsync(User);
            }
            catch (EntityNotFoundException<User> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }


        [Test]
        public async Task DeleteUser()
        {
            var result = await _UserRepository.DeleteAsync(1);
            Assert.IsTrue(result);
            try
            {
                var res = await _UserRepository.GetAsync(1);
            }
            catch (EntityNotFoundException<User> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task DeleteUserNotFound()
        {
            try
            {
                var result = await _UserRepository.DeleteAsync(3);
            }
            catch (EntityNotFoundException<User> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }   
    }
}
