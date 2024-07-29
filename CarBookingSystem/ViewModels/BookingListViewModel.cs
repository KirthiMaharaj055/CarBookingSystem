using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarBookingSystem.Models;

namespace CarBookingSystem.ViewModels
{
    public class BookingListViewModel
    {
        public IEnumerable<Booking> Bookings { get; set; }
    }
}