using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Domain.Enums;

namespace TrafficPenaltyManagement.Application.Dtos.VehicleDtos
{
    public class CreateVehicleDto
    {
        [Required(ErrorMessage = "Plaka alanı zorunludur.")]
        [RegularExpression(
     @"^(0[1-9]|[1-7][0-9]|8[01]) [A-ZÇĞİÖŞÜ]{1,3} \d{2,4}$",
     ErrorMessage = "Geçerli bir Türkiye plakası giriniz. Örn: 41 ABC 123")]
        public string Plate { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public VehicleType VehicleType { get; set; }
    }
}
