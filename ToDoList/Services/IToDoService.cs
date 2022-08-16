using ToDoList.Models;
using ToDoList.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Services
{
    public interface IToDoService
    {
        void Edit(ToDo toDo);
        void Delete(int id);
        ToDo GetById(int id);
        ToDoDTO GetDtoById(int id);
        void Create(ToDo actor, User user);
        List<ToDoDTO> GetAll();
        List<ToDoDTO> GetUserToDoes(int id);
    }
}
