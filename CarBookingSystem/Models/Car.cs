using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace CarBookingSystem.Models
{
    [Collection("cars")]
    public class Car
    {
        public ObjectId Id { get; set; }

        [Required(ErrorMessage = "You must provide the make and model")]
        [Display(Name = "Make and Model")]
        public string? Model { get; set; }

        [Required(ErrorMessage = "The number plate is required to identify the vehicle")]
        [Display(Name = "Number Plate")]
        public string NumberPlate { get; set; }

        [Required(ErrorMessage = "You must add the location of the car")]
        public string? Location { get; set; }

        public bool IsBooked { get; set; } = false;
    }
}