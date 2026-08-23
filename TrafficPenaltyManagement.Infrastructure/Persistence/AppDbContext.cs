using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Domain.Entitites;
using TrafficPenaltyManagement.Infrastructure.Identitiy;

namespace TrafficPenaltyManagement.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PenaltyApprovalHistory> PenaltyApprovalHistories { get; set; }
    }
}
