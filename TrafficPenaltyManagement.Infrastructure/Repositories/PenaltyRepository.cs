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
    public class PenaltyRepository : GenericRepository<Penalty>, IPenaltyRepository
    {
        private readonly AppDbContext _context;

        public PenaltyRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Penalty>> GetAllAsync()
        {
            return await _context.Penalties.Include(x => x.Vehicle).ThenInclude(x => x.VehicleAssignments).ThenInclude(x => x.Employee).Include(x =>x.PenaltyType).ToListAsync();
        }

        public async Task<Penalty?> GetByIdAsync(int id)
        {
            return await _context.Penalties.Include(x => x.Vehicle).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Penalty>> GetByVehicleIdAsync(int vehicleId)
        {
            return await _context.Penalties.Where(x => x.VehicleId == vehicleId).ToListAsync();
        }

    }
}
