using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Application.Dtos.EmployeeDtos;

namespace TrafficPenaltyManagement.Application.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<EmployeeDto?> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto);
        Task<List<EmployeeDto>> GetEmployeesAsync(string department, string branch);
    }
}