using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Domain.Enums;

namespace TrafficPenaltyManagement.Infrastructure.Identitiy
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public UserRole Role { get; set; }
    }
}
