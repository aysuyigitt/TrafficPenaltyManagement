using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Domain.Enums;

namespace TrafficPenaltyManagement.Domain.Entitites
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string Plate { get; set; }

        public VehicleType VehicleType { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }
        public ICollection<Penalty> Penalties { get; set; }
    }
}
