using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Application.Dtos.PenaltyDtos;
using TrafficPenaltyManagement.Domain.Entitites;

namespace TrafficPenaltyManagement.Application.Services.PenaltyTypeServices
{
    public interface IPenaltyTypeService
    {
        Task<PenaltyTypeDto> CreatePenaltyTypeAsync(CreatePenaltyTypeDto createPenaltyTypeDto);
        Task<List<PenaltyTypeDto>> GetAllPenaltyTypeAsync();
        Task<PenaltyTypeDto?> GetByIdAsync(int id);
        Task UpdatePenaltyTypeAsync(UpdatePenaltyTypeDto updatePenaltyTypeDto);
        Task DeletePenaltyTypeAsync(int id);
    }
}
