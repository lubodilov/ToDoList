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

        [Test]
        public void TestGetToDoByName()
        {
            DateTime today = DateTime.Today;
            ToDo toDo5 = CreateToDo(5, "Work 5", "describtion 5", "easy 5", today, today, userId);
            ToDo toDo6 = CreateToDo(6, "Work 6", "describtion 6", "easy 6", today, today, userId);
            ToDo toDo7 = CreateToDo(7, "Work 7", "describtion 7", "easy 7", today, today, userId);
            User user = new User();

            toDoService.Create(toDo5, user);
            toDoService.Create(toDo6, user);
            toDoService.Create(toDo7, user);
            string name = toDo6.Name;
            List<ToDoDTO> toDoes = toDoService.GetUserToDoesName(userId, name);


            Assert.AreEqual(1, toDoes.Count);
            Assert.AreEqual("easy 6", toDoes[0].Difficulty);
        }

        [Test]
        public void TestGetToDoByDifficulty()
        {
            DateTime today = DateTime.Today;
            ToDo toDo5 = CreateToDo(5, "Work 5", "describtion 5", "easy 5", today, today, userId);
            ToDo toDo6 = CreateToDo(6, "Work 6", "describtion 6", "easy 6", today, today, userId);
            ToDo toDo7 = CreateToDo(7, "Work 7", "describtion 7", "easy 7", today, today, userId);
            User user = new User();

            toDoService.Create(toDo5, user);
            toDoService.Create(toDo6, user);
            toDoService.Create(toDo7, user);
            string name = toDo6.Difficulty;
            List<ToDoDTO> toDoes = toDoService.GetUserToDoesDifficulty(userId, name);


            Assert.AreEqual(1, toDoes.Count);
            Assert.AreEqual("Work 6", toDoes[0].Name);
        }

        [Test]
        public void TestSortToDoByName()
        {
            DateTime today = DateTime.Today;
            ToDo toDo5 = CreateToDo(5, "AAA", "describtion 5", "easy 5", today, today, userId);
            ToDo toDo6 = CreateToDo(6, "CCC", "describtion 6", "easy 6", today, today, userId);
            ToDo toDo7 = CreateToDo(7, "BBB", "describtion 7", "easy 7", today, today, userId);
            User user = new User();

            toDoService.Create(toDo5, user);
            toDoService.Create(toDo6, user);
            toDoService.Create(toDo7, user);
            
            List<ToDoDTO> toDoes = toDoService.GetToDoSortName(userId);


            Assert.AreEqual("easy 5", toDoes[0].Difficulty);
            Assert.AreEqual("easy 7", toDoes[1].Difficulty);
            Assert.AreEqual("easy 6", toDoes[2].Difficulty);
        }

        [Test]
        public void TestSortToDoByNameDesc()
        {
            DateTime today = DateTime.Today;
            ToDo toDo5 = CreateToDo(5, "XXX", "describtion 5", "easy 5", today, today, userId);
            ToDo toDo6 = CreateToDo(6, "ZZZ", "describtion 6", "easy 6", today, today, userId);
            ToDo toDo7 = CreateToDo(7, "YYY", "describtion 7", "easy 7", today, today, userId);
            User user = new User();

            toDoService.Create(toDo5, user);
            toDoService.Create(toDo6, user);
            toDoService.Create(toDo7, user);

            List<ToDoDTO> toDoes = toDoService.GetToDoSortNameDesc(userId);


            //Assert.AreEqual(3, toDoes.Count);
            Assert.AreEqual("ZZZ", toDoes[0].Name);
            Assert.AreEqual("YYY", toDoes[1].Name);
            Assert.AreEqual("XXX", toDoes[2].Name);
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