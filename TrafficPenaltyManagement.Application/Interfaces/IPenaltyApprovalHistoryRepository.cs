using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Domain.Entitites;

namespace TrafficPenaltyManagement.Application.Interfaces
{
    public interface IPenaltyApprovalHistoryRepository
    {
        Task<PenaltyApprovalHistory> AddAsync(PenaltyApprovalHistory history);

        Task<List<PenaltyApprovalHistory>> GetByPenaltyIdAsync(int penaltyId);
    }
}
