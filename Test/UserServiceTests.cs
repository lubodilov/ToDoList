using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Services;
using ToDoList.Models.DTOs;

namespace Test
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService userService;
        private UserDbContext context;

        private User user;


        [SetUp]
        public void Setup()
        {

            var options = new DbContextOptionsBuilder<UserDbContext>()
               .UseInMemoryDatabase("TestDb").Options;

            this.context = new UserDbContext(options);
            userService = new UserService(this.context);


            this.user = new User();
            user.Id = 1;
            user.Email = "asd@asd.com";
            context.Users.Add(user);
            context.SaveChanges();

        }

        [Test]
        public void TestUpdate()
        {

            UserDTO userDTO = new UserDTO();
            userDTO.FirstName = "Alex";
            userDTO.LastName = "Ivanova";
            userDTO.City = "Sofia";
            userService.Update(user.Id, userDTO);

            User userDB = context.Users.FirstOrDefault(u => u.Id == user.Id);

            Assert.NotNull(userDB);
            Assert.AreEqual(userDB.FirstName, "Alex");
            Assert.AreEqual(userDB.LastName, "Ivanova");
            Assert.AreEqual(userDB.City, "Sofia");

        }

        [Test]
        public void TestDelete()
        {

            userService.Delete(user.Id);

            User userDB = context.Users.FirstOrDefault(u => u.Id == user.Id);

            Assert.Null(userDB);
        }
        [Test]
        public void TestGetEntityById()
        {

            User userDB = userService.GetEntityById(user.Id);

            Assert.AreEqual(userDB.Email, "asd@asd.com");
        }
        [Test]
        public void TestGetById()
        {
            UserDTO userDB = userService.GetById(user.Id);

            Assert.AreEqual(userDB.Email, "asd@asd.com");
        }


        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
        }

    }
}