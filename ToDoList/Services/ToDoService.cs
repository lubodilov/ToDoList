using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Services
{
    //
    //Summary:
    //  Implements CRUD operations with the DB for the Class ToDo
    //
    public class ToDoService : IToDoService
    {
        private UserDbContext dbContext;
        public ToDoService(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //
        //Summary:
        //  Creates a new toDo and ads it to the DB
        //
        public void Create(ToDo toDo, User user)
        {
            toDo.User = user;
            dbContext.ToDoes.Add(toDo);
            dbContext.SaveChanges();
        }
        //
        //Summary:
        //  Deletes a toDo found by its id and removes it from the DB
        //
        public void Delete(int id)
        {
            dbContext.ToDoes.Remove(GetById(id));
            dbContext.SaveChanges();
        }
        //
        //Summary:
        //  Edits a toDo and updates the DB
        //
        public void Edit(ToDo toDo)
        {
            ToDo oldToDo = GetById(toDo.Id);
            oldToDo.Name = toDo.Name;
            oldToDo.Describtion= toDo.Describtion;
            oldToDo.Difficulty = toDo.Difficulty;
            oldToDo.StartDate = toDo.StartDate;
            oldToDo.EndDate = toDo.EndDate;

            dbContext.SaveChanges();
        }
        //
        //Summary:
        //  Finds a toDo by Id
        //
        public ToDo GetById(int id)
        {
            return dbContext.ToDoes
                .FirstOrDefault(p => p.Id == id);
        }

        //
        //Summary:
        //  Finds a toDoDTO by Id
        //
        public ToDoDTO GetDtoById(int id)
        {
            return ToDto(dbContext.ToDoes
                .FirstOrDefault(p => p.Id == id));
        }

        //
        //Summary:
        //  Returns all toDoes in the DB
        //
        public List<ToDoDTO> GetAll()
        {
            return dbContext.ToDoes
                .Select(a => ToDto(a))
                .ToList();
        }
        //
        //Summary:
        //  Returns all toDoes having the given userId
        //
        public List<ToDoDTO> GetUserToDoes(int id)
        {
            return dbContext.ToDoes
                .Where(p => p.UserId == id)
                .Select(p => ToDto(p))
                .ToList<ToDoDTO>();
        }
        public List<ToDoDTO> GetUserToDoesSearch(int id, string SearchPrase)
        {
            return dbContext.ToDoes
                .Where(p => p.UserId == id && (p.Name == SearchPrase || p.Difficulty == SearchPrase))
                .Select(p => ToDto(p))
                .ToList<ToDoDTO>();
        }
        private static ToDoDTO ToDto(ToDo t)
        {
            ToDoDTO toDo = new ToDoDTO();

            toDo.Id = t.Id;
            toDo.Name = t.Name;
            toDo.Describtion = t.Describtion;
            toDo.Difficulty = t.Difficulty;
            toDo.StartDate = t.StartDate;
            toDo.EndDate = t.EndDate;
            if (t.User != null)
            {
                toDo.CreatedBy = $"{t.User.FirstName} {t.User.LastName}";
                toDo.UserEmail = t.User.Email;
            }

            return toDo;
        }

    }
}
