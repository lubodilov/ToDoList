using ToDoList.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Data
{
    public class UserDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<ToDo> ToDoes { get; set; }
        public UserDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
