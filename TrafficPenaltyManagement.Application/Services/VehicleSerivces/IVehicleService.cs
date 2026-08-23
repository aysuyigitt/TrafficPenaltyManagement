using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Application.Dtos.VehicleDtos;

namespace TrafficPenaltyManagement.Application.Services.VehicleSerivces
{
    public interface IVehicleService
    {
        Task<VehicleDto?> CreateVehicleAsync(CreateVehicleDto createVehicleDto);
        Task<List<VehicleDto>> GetAllVehiclesAsync();

    }
}
