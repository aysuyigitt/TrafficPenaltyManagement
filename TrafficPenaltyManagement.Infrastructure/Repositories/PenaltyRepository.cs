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
    public class PenaltyRepository : IPenaltyRepository
    {
        private readonly AppDbContext _context;

        public PenaltyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Penalty> AddAsync(Penalty penalty)
        {
            await _context.Penalties.AddAsync(penalty);
            await _context.SaveChangesAsync();

            return penalty;
        }

        public async Task<List<Penalty>> GetAllAsync()
        {
            return await _context.Penalties.Include(x => x.Vehicle).ToListAsync();
        }

        public async Task<Penalty?> GetByIdAsync(int id)
        {
            return await _context.Penalties.Include(x => x.Vehicle).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Penalty> UpdateAsync(Penalty penalty)
        {
            _context.Penalties.Update(penalty);

            await _context.SaveChangesAsync();

            return penalty;
        }
    }
}
