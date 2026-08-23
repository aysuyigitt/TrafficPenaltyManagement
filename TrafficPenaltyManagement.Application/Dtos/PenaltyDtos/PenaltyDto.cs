using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Domain.Enums;

namespace TrafficPenaltyManagement.Application.Dtos.PenaltyDtos
{
    public class PenaltyDto
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public string? Plate { get; set; }

        public PenaltyStatus Status { get; set; }

        public string? RejectionReason { get; set; }
    }
}