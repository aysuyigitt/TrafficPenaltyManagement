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
    public class PenaltyApprovalHistoryRepository : GenericRepository<PenaltyApprovalHistory>, IPenaltyApprovalHistoryRepository
    {
        private readonly AppDbContext _context;

        public PenaltyApprovalHistoryRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<PenaltyApprovalHistory>> GetByPenaltyIdAsync(int penaltyId)
        {
            return await _context.PenaltyApprovalHistories.Where(x => x.PenaltyId == penaltyId).OrderByDescending(x => x.ActionDate).ToListAsync();
        }
    }
}
