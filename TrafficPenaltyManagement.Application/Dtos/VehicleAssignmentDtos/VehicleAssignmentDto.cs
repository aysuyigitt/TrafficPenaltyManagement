using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficPenaltyManagement.Application.Dtos.VehicleAssignmentDtos
{
    public class VehicleAssignmentDto
    {
        public int VehicleId { get; set; }
        public string Plate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
