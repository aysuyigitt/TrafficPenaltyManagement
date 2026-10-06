using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TrafficPenaltyManagement.Application.Dtos.PenaltyDtos
{
    public class CreatePenaltyDto
    {
        public string Plate { get; set; }
        public decimal Amount { get; set; }
        public int EmployeeId { get; set; }
        public int? PenaltyTypeId { get; set; }
        public DateTime PenaltyDate { get; set; }
        public IFormFile? Document { get; set; }

    }
}
