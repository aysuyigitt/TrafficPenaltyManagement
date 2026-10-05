using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Domain.Entitites;

namespace TrafficPenaltyManagement.Application.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
    //    Task<Employee> AddAsync(Employee employee);
    //    Task<List<Employee>> GetAllAsync();
    //    Task<Employee?> GetByIdAsync(int id);
    //    Task UpdateAsync(Employee employee);
        Task AddVehicleAssignmentAsync(VehicleAssignment vehicleAssignment);
        Task UpdateVehicleAssignmentAsync(VehicleAssignment vehicleAssignment);
        Task<List<Employee>> GetEmployeesAsync(string department, string branch);
    }
}
