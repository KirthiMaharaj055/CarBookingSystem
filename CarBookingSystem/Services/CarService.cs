using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace CarBookingSystem.Services
{
    public class CarService : ICarService
    {
        private readonly CarBookingDbContext _carDbContext;
        public CarService(CarBookingDbContext carDbContext)
        {
            _carDbContext = carDbContext;
        }

        public async Task AddCarAsync(Car car)
        {
            _carDbContext.Cars.Add(car);

            _carDbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_carDbContext.ChangeTracker.DebugView.LongView);

            _carDbContext.SaveChanges();
        }

        public async Task DeleteCarAsync(Car car)
        {
            var carToDelete = _carDbContext.Cars.Where(c => c.Id == car.Id).FirstOrDefault();

            if(carToDelete != null) {
            _carDbContext.Cars.Remove(carToDelete);
            _carDbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_carDbContext.ChangeTracker.DebugView.LongView);
            _carDbContext.SaveChanges();
            }
            else {
                throw new ArgumentException("The car to delete cannot be found.");
            }
        }

        public async Task EditCarAsync(Car car)
        {
            var carToUpdate = _carDbContext.Cars.FirstOrDefault(c => c.Id == car.Id);

            if(carToUpdate != null)
            {                
                carToUpdate.Model = car.Model;
                carToUpdate.NumberPlate = car.NumberPlate;
                carToUpdate.Location = car.Location;
                carToUpdate.IsBooked = car.IsBooked;

                _carDbContext.Cars.Update(carToUpdate);

                _carDbContext.ChangeTracker.DetectChanges();
                Console.WriteLine(_carDbContext.ChangeTracker.DebugView.LongView);

                _carDbContext.SaveChanges();
                    
            }
            else
            {
                throw new ArgumentException("The car to update cannot be found. ");
            }
        }        


        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            //return await _carDbContext.Cars.OrderBy(c => c.Id).AsNoTracking().AsAsyncEnumerable<Car>();
            return await _carDbContext.Cars.OrderBy(c => c.Id).AsNoTracking().ToListAsync();
        }

        public async Task<Car?> GetCarByIdAsync(ObjectId id)
        {
            return await _carDbContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}