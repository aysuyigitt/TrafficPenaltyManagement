using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficPenaltyManagement.Application.Dtos.EmployeeDtos
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        public string EMail { get; set; }

        public string Branch { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public int? VehicleId { get; set; }

    }
}
