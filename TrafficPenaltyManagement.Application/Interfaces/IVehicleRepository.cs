using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Domain.Entitites;

namespace TrafficPenaltyManagement.Application.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> AddAsync(Vehicle vehicle);
        Task<List<Vehicle>> GetAllAsync();
        Task<Vehicle?> GetByPlateAsync(string plate);
    }
}
