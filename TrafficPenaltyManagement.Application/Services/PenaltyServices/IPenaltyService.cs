using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Application.Dtos.PenaltyApprovalHistoryDtos;
using TrafficPenaltyManagement.Application.Dtos.PenaltyDtos;
using TrafficPenaltyManagement.Domain.Enums;

namespace TrafficPenaltyManagement.Application.Services.PenaltyServices
{
    public interface IPenaltyService
    {
        Task<PenaltyDto> CreatePenaltyAsync(CreatePenaltyDto createPenaltyDto);

        Task<PenaltyDto> ApprovePenaltyAsync(int penaltyId, string userId, UserRole userRole);

        Task<PenaltyDto> RejectPenaltyAsync(int penaltyId, string rejectionReason, string userId, UserRole userRole);

        Task<List<PenaltyApprovalHistoryDto>> GetPenaltyHistoryAsync(int penaltyId);

        Task<List<PenaltyDto>> GetAllPenaltiesAsync();

        Task<PenaltyDto?> GetPenaltyByIdAsync(int penaltyId);

        Task UpdatePenaltyAsync(UpdatePenaltyDto updatePenaltyDto);
    }
}
