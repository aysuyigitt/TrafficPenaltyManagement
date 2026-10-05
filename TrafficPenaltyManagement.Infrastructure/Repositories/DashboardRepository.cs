using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Application.Interfaces;
using TrafficPenaltyManagement.Domain.Enums;
using TrafficPenaltyManagement.Infrastructure.Persistence;
using TrafficPenaltyManagement.Application.Dtos.DashboardDtos;

namespace TrafficPenaltyManagement.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboardDataAsync()
        {
            var dashboard = new DashboardDto
            {
                TotalEmployees = await _context.Employees.CountAsync(),
                TotalVehicles = await _context.Vehicles.CountAsync(),
                TotalPenalties = await _context.Penalties.CountAsync(),
                CompletedPenalties = await _context.Penalties.CountAsync(x => x.Status == PenaltyStatus.Completed),
                NewPenalties = await _context.Penalties.CountAsync(x => x.Status == PenaltyStatus.New),
                ManagerApprovalPenalties = await _context.Penalties.CountAsync(x => x.Status == PenaltyStatus.ManagerApproval),
                FinanceApprovalPenalties = await _context.Penalties.CountAsync(x => x.Status == PenaltyStatus.FinanceApproval),
                RejectedPenalties = await _context.Penalties.CountAsync(x => x.Status == PenaltyStatus.Rejected)
            };
            return dashboard;
        }
    }
}
    
