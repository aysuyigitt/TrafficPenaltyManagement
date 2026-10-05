using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Application.Dtos.EmployeeDtos;
using TrafficPenaltyManagement.Application.Dtos.PenaltyDtos;
using TrafficPenaltyManagement.Application.Dtos.VehicleAssignmentDtos;
using TrafficPenaltyManagement.Application.Interfaces;
using TrafficPenaltyManagement.Domain.Entitites;

namespace TrafficPenaltyManagement.Application.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPenaltyRepository _penaltyRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IPenaltyRepository penaltyRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _penaltyRepository = penaltyRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto?> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            var employee = _mapper.Map<Employee>(createEmployeeDto);

            var createdEmployee = await _employeeRepository.AddAsync(employee);

            if (createEmployeeDto.VehicleId.HasValue)
            {
                var vehicleAssignment = new VehicleAssignment
                {
                    EmployeeId = createdEmployee.Id,
                    VehicleId = createEmployeeDto.VehicleId.Value,
                    StartDate = DateTime.Now,
                    EndDate = null
                };

                await _employeeRepository.AddVehicleAssignmentAsync(vehicleAssignment);
            }

            return _mapper.Map<EmployeeDto>(createdEmployee);
        }

           public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
           {
            var employees = await _employeeRepository.GetAllAsync();

            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            foreach (var employeeDto in employeeDtos)
            {
                var employee = employees.FirstOrDefault(x => x.Id == employeeDto.Id);

                if (employee != null)
                {
                    var assignment = employee.VehicleAssignments?.FirstOrDefault(x => x.EndDate == null);

                    employeeDto.VehicleId = assignment?.VehicleId;
                    employeeDto.Plate = assignment?.Vehicle?.Plate;
                }
            }

            return employeeDtos;
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return null;
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            // Aktif araç 
            var assignment = employee.VehicleAssignments?
                .FirstOrDefault(x => x.EndDate == null);

            // Çalışanın kullandığı araçların,
            // kullandığı dönemdeki trafik cezalarını getir
            var employeePenalties = new List<Penalty>();

            foreach (var vehicleAssignment in employee.VehicleAssignments ?? new List<VehicleAssignment>())
            {
                var penalties = await _penaltyRepository
                    .GetByVehicleIdAsync(vehicleAssignment.VehicleId);

                var assignmentPenalties = penalties
                   .Where(x =>
                       x.PenaltyDate >= vehicleAssignment.StartDate &&
                   (
                        vehicleAssignment.EndDate == null ||
                        x.PenaltyDate <= vehicleAssignment.EndDate.Value
                   )
            )
           .ToList();

                employeePenalties.AddRange(assignmentPenalties);
            }

            employeeDto.Penalties = _mapper.Map<List<PenaltyDto>>(employeePenalties);

            // Aktif araç bilgisi
            employeeDto.VehicleId = assignment?.VehicleId;
            employeeDto.Plate = assignment?.Vehicle?.Plate;

            // Araç geçmişi
            employeeDto.VehicleAssignments = employee.VehicleAssignments?
                .Select(x => new VehicleAssignmentDto
                {
                    VehicleId = x.VehicleId,
                    Plate = x.Vehicle?.Plate,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                })
                .ToList() ?? new List<VehicleAssignmentDto>();

            return employeeDto;
        }


        public async Task UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(updateEmployeeDto.Id);

            if (employee == null)
            {
                return;
            }

            _mapper.Map(updateEmployeeDto, employee);

            var currentAssignment = employee.VehicleAssignments?.FirstOrDefault(x => x.EndDate == null);

            if (currentAssignment != null && currentAssignment.VehicleId != updateEmployeeDto.VehicleId)
            {
                currentAssignment.EndDate = DateTime.Now;

                await _employeeRepository.UpdateVehicleAssignmentAsync(currentAssignment);
            }

            if (updateEmployeeDto.VehicleId.HasValue && (currentAssignment == null || currentAssignment.VehicleId != updateEmployeeDto.VehicleId))
            {
                var newAssignment = new VehicleAssignment
                {
                    EmployeeId = employee.Id,
                    VehicleId = updateEmployeeDto.VehicleId.Value,
                    StartDate = DateTime.Now,
                    EndDate = null
                };

                await _employeeRepository.AddVehicleAssignmentAsync(newAssignment);
            }

            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task<List<EmployeeDto>> GetEmployeesAsync(string department, string branch)
        {
            var employees = await _employeeRepository.GetEmployeesAsync(department, branch);
            return _mapper.Map<List<EmployeeDto>>(employees);
        }
    }
}