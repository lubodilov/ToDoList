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
    public class ToDoServiceTests
    {
        private ToDoService toDoService;

        private UserDbContext context;

        private ToDo toDo;
        private int userId = 1;

        [SetUp]
        public void Setup()
        {

            var options = new DbContextOptionsBuilder<UserDbContext>()
               .UseInMemoryDatabase("TestDb").Options;

            this.context = new UserDbContext(options);
            toDoService = new ToDoService(this.context);

            DateTime today = DateTime.Today;
            this.toDo = CreateToDo(1, "Work", "description", "easy", today, today, userId);

            context.ToDoes.Add(toDo);
            context.SaveChanges();

        }

        [Test]
        public void TestCreate()
        {
            DateTime today = DateTime.Today;
            ToDo toDo = CreateToDo(2, "Work 2", "description 2", "easy", today, today, userId);
            User user = new User();
            
            toDoService.Create(toDo, user);

            ToDo toDoDB = context.ToDoes.FirstOrDefault(p => p.Id == toDo.Id);

            Assert.NotNull(toDoDB);
        }

        [Test]
        public void TestEdit()
        {
            DateTime today = DateTime.Today;
            ToDo toDo = CreateToDo(1, "Work", "describtion changed", "easy", today, today, userId);

            toDoService.Edit(toDo);

            ToDo toDoDB = context.ToDoes.FirstOrDefault(p => p.Id == toDo.Id);

            Assert.NotNull(toDoDB);
            Assert.AreEqual(toDoDB.Describtion, "describtion changed");

        }

        [Test]
        public void TestDelete()
        {
            toDoService.Delete(toDo.Id);

            ToDo toDoDB = context.ToDoes.FirstOrDefault(p => p.Id == toDo.Id);

            Assert.Null(toDoDB);
        }


        [Test]
        public void TestGetAll()
        {
            DateTime today = DateTime.Today;
            ToDo toDo2 = CreateToDo(2, "Work 2", "describtion 2", "easy 2", today, today, userId);
            ToDo toDo3 = CreateToDo(3, "Work 3", "describtion 3", "easy 3", today, today, userId);
            User user = new User();
            
            toDoService.Create(toDo2, user);
            toDoService.Create(toDo3, user);

            List<ToDoDTO> toDoes = toDoService.GetAll();

            Assert.AreEqual(3, toDoes.Count);
            Assert.AreEqual("Work 2", toDoes[1].Name);

        }


        [Test]
        public void TestGetById()
        {

            ToDo toDoDB = toDoService.GetById(toDo.Id);

            Assert.AreEqual(toDoDB.Describtion, "description");
        }


        [Test]
        public void TestGetUserToDoes()
        {
             DateTime today = DateTime.Today;
            ToDo toDo2 = CreateToDo(3, "Work 3", "describtion 3", "easy 3", today, today, 2);
            ToDo toDo3 = CreateToDo(4, "Work 4", "describtion 4", "easy 4", today, today, userId);
            User user = new User();
            
            toDoService.Create(toDo2, user);
            toDoService.Create(toDo3, user);

            List<ToDoDTO> toDoes = toDoService.GetUserToDoes(userId);


            Assert.AreEqual(3, toDoes.Count);
            Assert.AreEqual("Work", toDoes[0].Name);
        }


        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
        }


        private ToDo CreateToDo(int id, string name, string describtion, string difficulty, DateTime startDate, DateTime endDate, int userId)
        {
            ToDo toDo = new ToDo();
            toDo.Id = id;
            toDo.Name = name;
            toDo.Describtion = describtion;
            toDo.Difficulty = difficulty;
            toDo.StartDate = startDate;
            toDo.EndDate = endDate;
            toDo.UserId = userId;

            return toDo;
        }

    }
}