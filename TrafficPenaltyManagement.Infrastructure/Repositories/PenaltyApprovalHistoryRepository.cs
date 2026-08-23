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
    public class PenaltyApprovalHistoryRepository : IPenaltyApprovalHistoryRepository
    {
        private readonly AppDbContext _context;

        public PenaltyApprovalHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PenaltyApprovalHistory> AddAsync(PenaltyApprovalHistory history)
        {
            await _context.PenaltyApprovalHistories.AddAsync(history);
            await _context.SaveChangesAsync();

            return history;
        }

        public async Task<List<PenaltyApprovalHistory>> GetByPenaltyIdAsync(int penaltyId)
        {
            return await _context.PenaltyApprovalHistories.Where(x => x.PenaltyId == penaltyId).OrderByDescending(x => x.ActionDate).ToListAsync();
        }
    }
}
