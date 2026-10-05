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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;
         
        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddVehicleAssignmentAsync(VehicleAssignment vehicleAssignment)
        {
            await _context.VehicleAssignments.AddAsync(vehicleAssignment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.Include(x=>x.VehicleAssignments).ThenInclude(x => x.Vehicle).ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.Include(x => x.VehicleAssignments).ThenInclude(x => x.Vehicle).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateVehicleAssignmentAsync(VehicleAssignment vehicleAssignment)
        {
            _context.VehicleAssignments.Update(vehicleAssignment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetEmployeesAsync(string department, string branch)
       {
            var query = _context.Employees.Include(x => x.VehicleAssignments).ThenInclude(x => x.Vehicle).AsQueryable();

            if (!string.IsNullOrWhiteSpace(department))
            {
                query = query.Where(x => x.Department == department);
            }

            if (!string.IsNullOrWhiteSpace(branch))
            {
                query = query.Where(x => x.Branch == branch);
            }

            return await query.ToListAsync();

        }
    }
}
