using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Domain.Enums;

namespace TrafficPenaltyManagement.Application.Dtos.PenaltyApprovalHistoryDtos
{
    public class PenaltyApprovalHistoryDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime ActionDate { get; set; }

        public ApprovalActionType ActionType { get; set; }

        public string? Description { get; set; }

        public PenaltyStatus PreviousStatus { get; set; }

        public PenaltyStatus NewStatus { get; set; }
    }
}
