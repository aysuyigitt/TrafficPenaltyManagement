using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Application.Interfaces;
using TrafficPenaltyManagement.Domain.Entitites;
using TrafficPenaltyManagement.Infrastructure.Persistence;

namespace TrafficPenaltyManagement.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _context;

        public VehicleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> AddAsync(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        public async Task<List<Vehicle>> GetAllAsync()
        {
            return await _context.Vehicles.ToListAsync();

        }

        public async Task<Vehicle?> GetByPlateAsync(string plate)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(x => x.Plate == plate);
        }
    }
}
