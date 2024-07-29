using System.Collections.Generic;
using System.Threading.Tasks;
using CarBookingSystem.Models;
using MongoDB.Bson;

namespace CarBookingSystem.Services
{
    public interface ICarService
    {
        Task AddCarAsync(Car car);
        Task DeleteCarAsync(Car car);
        Task EditCarAsync(Car car);
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<Car?> GetCarByIdAsync(ObjectId id);
    }
}
