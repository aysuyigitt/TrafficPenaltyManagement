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
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly AppDbContext _context;

        public VehicleRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Vehicle?> GetByPlateAsync(string plate)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(x => x.Plate == plate);
        }
    }
}
