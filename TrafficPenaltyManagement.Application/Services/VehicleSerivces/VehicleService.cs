using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Application.Dtos.VehicleDtos;
using TrafficPenaltyManagement.Application.Interfaces;
using TrafficPenaltyManagement.Domain.Entitites;

namespace TrafficPenaltyManagement.Application.Services.VehicleSerivces
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<VehicleDto?> CreateVehicleAsync(CreateVehicleDto createVehicleDto)
        {
            var existingVehicle = await _vehicleRepository.GetByPlateAsync(createVehicleDto.Plate);

            if (existingVehicle != null)
            {
                return null;
            }

            var vehicle = _mapper.Map<Vehicle>(createVehicleDto);

            var createdVehicle = await _vehicleRepository.AddAsync(vehicle);

            return _mapper.Map<VehicleDto>(createdVehicle);
        }

        public async Task<List<VehicleDto>> GetAllVehiclesAsync()
        {
            var vehicles = await _vehicleRepository.GetAllAsync();

            return _mapper.Map<List<VehicleDto>>(vehicles);
        }
    }
}
