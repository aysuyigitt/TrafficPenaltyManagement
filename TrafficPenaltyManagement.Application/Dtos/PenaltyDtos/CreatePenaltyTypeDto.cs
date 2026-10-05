using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficPenaltyManagement.Application.Dtos.PenaltyDtos
{
    public class CreatePenaltyTypeDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
