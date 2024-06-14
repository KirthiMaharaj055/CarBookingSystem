using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarBookingSystem.Models;
using MongoDB.Bson;

namespace CarBookingSystem.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(ObjectId id);
        Task AddBookingAsync(Booking newBooking);
        Task EditBookingAsync(Booking updatedBooking);
        Task DeleteBookingAsync(Booking bookingToDelete);
    }
}