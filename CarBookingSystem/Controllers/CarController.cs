using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarBookingSystem.Models;
using CarBookingSystem.Services;
using CarBookingSystem.ViewModels;
using MongoDB.Bson;

namespace CarBookingSystem.Controllers
{
    public class CarController : Controller
    {
        private readonly ILogger<CarController> _logger;
        private readonly ICarService _carService;

        public CarController(ILogger<CarController> logger, ICarService carService)
        {
            _logger = logger;
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllCarsAsync();
            var viewModel = new CarListViewModel { Cars = cars };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarAddViewModel carAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var newCar = new Car
                {
                    Model = carAddViewModel.Car.Model,
                    Location = carAddViewModel.Car.Location,
                    NumberPlate = carAddViewModel.Car.NumberPlate
                };

                await _carService.AddCarAsync(newCar);
                return RedirectToAction("Index");
            }

            return View(carAddViewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var selectedCar = await _carService.GetCarByIdAsync(new ObjectId(id));
            if (selectedCar == null)
            {
                return NotFound();
            }

            return View(selectedCar);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _carService.EditCarAsync(car);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Updating the car failed, please try again! Error: {ex.Message}");
                }
            }
            return View(car);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
