using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Domain.Enums;

namespace TrafficPenaltyManagement.Domain.Entitites
{
    public class PenaltyApprovalHistory
    {
        public int Id { get; set; }

        public int PenaltyId { get; set; }

        public string UserId { get; set; }

        public DateTime ActionDate { get; set; }

        public ApprovalActionType ActionType { get; set; }

        public string? Description { get; set; }

        public PenaltyStatus PreviousStatus { get; set; }

        public PenaltyStatus NewStatus { get; set; }
    }
}