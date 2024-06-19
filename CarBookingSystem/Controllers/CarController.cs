using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarBookingSystem.Models;
using CarBookingSystem.Services;
using CarBookingSystem.ViewModels;
using MongoDB.Bson;

namespace CarBookingSystem.Controllers;

public class CarController : Controller
{
    private readonly ILogger<CarController> _logger;
    private readonly ICarService _carService;

    public CarController(ILogger<CarController> logger,ICarService carService)
    {
        _logger = logger;
        _carService = carService;
    }

    public  async Task<IActionResult> Index()
    {
        CarListViewModel viewModel = new()
        {
            Cars = await _carService.GetAllCarsAsync(),
        };
        return View(viewModel);
    }

    public async Task<IActionResult> Add()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Add(CarAddViewModel carAddViewModel)
    {
        if (ModelState.IsValid)
        {
            Car newCar = new()
            {
                Model = carAddViewModel.Car.Model,
                Location = carAddViewModel.Car.Location,
                NumberPlate = carAddViewModel.Car.NumberPlate
            };

            await _carService.AddCarAsync(newCar); // Await the asynchronous method call

            return RedirectToAction("Index");
        }
        return View(carAddViewModel);      
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var selectedCar = await _carService.GetCarByIdAsync(new ObjectId(id)); // Await the asynchronous method call
        return View(selectedCar);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Car car)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _carService.EditCarAsync(car); // Await the asynchronous method call
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Updating the car failed, please try again! Error: {ex.Message}");
        }

        return View(car);
    }

    public async Task<IActionResult> Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
