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
        List<ToDoDTO> GetUserToDoesName(int id, string SearchPrase);
        List<ToDoDTO> GetUserToDoesDifficulty(int id, string SearchPrase);
        List<ToDoDTO> GetToDoSortName(int id);
        List<ToDoDTO> GetToDoSortNameDesc(int id);
        List<ToDoDTO> GetToDoSortDescribtion(int id);
        List<ToDoDTO> GetToDoSortDescribtionDesc(int id);
        List<ToDoDTO> GetToDoSortDifficulty(int id);
        List<ToDoDTO> GetToDoSortDifficultyDesc(int id);
        List<ToDoDTO> GetToDoSortStartDate(int id);
        List<ToDoDTO> GetToDoSortStartDateDesc(int id);
        List<ToDoDTO> GetToDoSortEndDate(int id);
        List<ToDoDTO> GetToDoSortEndDateDesc(int id);
    }
}
