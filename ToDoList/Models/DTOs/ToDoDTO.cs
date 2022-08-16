using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models.DTOs
{
    public class ToDoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The describtion is required.")]
        public string Describtion { get; set; }

        public string Difficulty { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CreatedBy { get; set; }

        public string UserEmail { get; set; }

        public ToDoDTO()
        {
                
        }

        public ToDoDTO(int id, string name, string describtion, string difficulty, DateTime startDate, DateTime endDate, string createdBy, string userEmail)
        {
            Id = id;
            Name = name;
            Describtion = describtion;
            Difficulty = difficulty;
            StartDate = startDate;
            EndDate = endDate;
            CreatedBy = createdBy;
            UserEmail = userEmail;
        }
    }
}
