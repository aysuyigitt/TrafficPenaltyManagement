using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Application.Interfaces;
using TrafficPenaltyManagement.Domain.Entitites;
using TrafficPenaltyManagement.Infrastructure.Persistence;

namespace TrafficPenaltyManagement.Infrastructure.Repositories
{
    public class PenaltyTypeRepository : GenericRepository<PenaltyType>, IPenaltyTypeRepository
    {
        private readonly AppDbContext _context;

        public PenaltyTypeRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

    }
}
