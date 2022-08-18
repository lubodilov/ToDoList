using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{   
    public class ToDo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Describtion { get; set; }

        public string Difficulty { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ToDo()
        {

        }

        public ToDo(int id, string name, string describtion, string difficulty, DateTime startDate, DateTime endDate, int userId, User user)
        {
            Id = id;
            Name = name;
            Describtion = describtion;
            Difficulty = difficulty;
            StartDate = startDate;
            EndDate = endDate;
            UserId = userId;
            User = user;
        }
    }
}
