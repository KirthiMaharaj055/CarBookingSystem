using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CarBookingSystem.Models;
using CarBookingSystem.Services;
using CarBookingSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace CarBookingSystem.Controllers
{
   // [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ICarService _carService;   

        public BookingController(IBookingService bookingService, ICarService carService)
        {
            _bookingService = bookingService;
            _carService = carService;
        }

        public async  Task<IActionResult> Index()
        {
            BookingListViewModel viewModel = new BookingListViewModel()
            {
                Bookings = await _bookingService.GetAllBookingsAsync()
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Add(string carId)
        {
            var selectedCar = await _carService.GetCarByIdAsync(new ObjectId(carId)); // Await the asynchronous method call

            BookingAddViewModel bookingAddViewModel = new BookingAddViewModel
            {
                Booking = new Booking
                {
                    CarId = selectedCar.Id,
                    CarModel = selectedCar.Model,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(1)
                }
            };

            return View(bookingAddViewModel);
         
        }

        [HttpPost]
        public async  Task<IActionResult> Add(BookingAddViewModel bookingAddViewModel)
        {
            Booking newBooking = new()
            {
                CarId = bookingAddViewModel.Booking.CarId,                   
                StartDate = bookingAddViewModel.Booking.StartDate,
                EndDate = bookingAddViewModel.Booking.EndDate,
            };

            await _bookingService.AddBookingAsync(newBooking);
            return RedirectToAction("Index");   
        }

        public async Task<IActionResult> Edit(string Id)
        {
            if(Id == null)
            {
                return NotFound();
            }

            var selectedBooking = await _bookingService.GetBookingByIdAsync(new ObjectId(Id));
            return View(selectedBooking);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Booking booking)
        {
            try
            {
                var existingBooking = _bookingService.GetBookingByIdAsync(booking.Id);
                if (existingBooking != null)
                {
                    await _bookingService.EditBookingAsync(booking);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", $"Booking with ID {booking.Id} does not exist!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Updating the booking failed, please try again! Error: {ex.Message}");
            }

            return View(booking);
        }

        public async Task<IActionResult> Delete(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var selectedBooking = await _bookingService.GetBookingByIdAsync(new ObjectId(Id));
            return View(selectedBooking);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Booking booking)
        {
            if(booking.Id == null)
            {
                ViewData["ErrorMessage"] = "Deleting the booking failed, invalid ID!";
                return View();
            }
            try
            {
                await _bookingService.DeleteBookingAsync(booking); // Await the asynchronous method call
                TempData["BookingDeleted"] = "Booking deleted successfully";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Deleting the booking failed, please try again! Error: {ex.Message}";
            }

            var selectedCar = await _bookingService.GetBookingByIdAsync(booking.Id); // Await the asynchronous method call
            return View(selectedCar);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
