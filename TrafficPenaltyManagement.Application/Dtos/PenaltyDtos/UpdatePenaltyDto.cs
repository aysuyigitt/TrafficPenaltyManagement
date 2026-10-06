using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace TrafficPenaltyManagement.Application.Dtos.PenaltyDtos
{
    public class UpdatePenaltyDto
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public string? Plate { get; set; }

        public decimal Amount { get; set; }

        public DateTime PenaltyDate { get; set; }

        public int? PenaltyTypeId { get; set; }
        public IFormFile? Document { get; set; }

        public string PenaltyTypeName { get; set; }

        public PenaltyStatus Status { get; set; }

        public string? RejectionReason { get; set; }

        public string? EmployeeName { get; set; }
    }
}
