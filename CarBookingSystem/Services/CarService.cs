using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarBookingSystem.Models;
using MongoDB.Bson;
using Microsoft.EntityFrameworkCore;

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
            await _carDbContext.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(Car car)
        {
            var carToDelete = await _carDbContext.Cars.FindAsync(car.Id);
            if (carToDelete != null)
            {
                _carDbContext.Cars.Remove(carToDelete);
                await _carDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("The car to delete cannot be found.");
            }
        }

        public async Task EditCarAsync(Car car)
        {
            var carToUpdate = await _carDbContext.Cars.FindAsync(car.Id);
            if (carToUpdate != null)
            {
                carToUpdate.Model = car.Model;
                carToUpdate.NumberPlate = car.NumberPlate;
                carToUpdate.Location = car.Location;
                carToUpdate.IsBooked = car.IsBooked;

                _carDbContext.Cars.Update(carToUpdate);
                await _carDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("The car to update cannot be found.");
            }
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _carDbContext.Cars.OrderBy(c => c.Id).AsNoTracking().ToListAsync();
        }

        public async Task<Car?> GetCarByIdAsync(ObjectId id)
        {
            return await _carDbContext.Cars.FindAsync(id);
        }
    }
}
