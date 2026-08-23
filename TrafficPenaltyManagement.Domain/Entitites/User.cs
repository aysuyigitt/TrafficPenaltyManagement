using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Domain.Enums;

namespace TrafficPenaltyManagement.Domain.Entitites
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UserRole Role { get; set; }
    }
}
