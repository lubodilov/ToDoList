using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Services
{
    public class UserService : IUserService
    {
        private UserDbContext dbContext;



        public UserService(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //
        // Summary:
        //    Implements CRUD operations with DB for the class User
        //


        //
        // Summary:
        //     Updates the DB for a User
        //

        public void Update(int id, UserDTO userDTO)
        {
            User user = this.GetEntityById(id);

            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.City = userDTO.City;

            dbContext.SaveChanges();
        }

        //
        // Summary:
        //     Deletes a User by passed id from the DB
        //
        public void Delete(int id)
        {
            User user = this.GetEntityById(id);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();

        }


        //
        // Summary:
        //     Finds a User by passed id from the DB
        //
        public User GetEntityById(int id)
        {
            return dbContext.Users.FirstOrDefault(u => u.Id == id);

        }

        //
        // Summary:
        //     Finds a User by passed id from the DB and sends it like UserDTO
        //
        public UserDTO GetById(int id)
        {
            return this.toDTO(dbContext.Users.FirstOrDefault(u => u.Id == id));

        }

        //
        // Summary:
        //     Returns a UserDTO by passed User
        //
        private UserDTO toDTO(User user)
        {
            return new UserDTO(
                user.Id, user.Email, user.PasswordHash,
                user.FirstName, user.LastName, user.City);
        }
    }
}
