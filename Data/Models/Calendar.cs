﻿using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Data.Models
{
    public class Calendar
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; } = null!;
    }
}
