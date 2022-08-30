using ToDoList.Models;
using ToDoList.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Services
{
    public interface IUserService
    {
        User GetEntityById(int id);
        UserDTO GetById(int id);
        void Update(int id, UserDTO userDTO);
        void Delete(int id);
    }
}
