using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Domain.Entitites;

namespace TrafficPenaltyManagement.Application.Interfaces
{
    public interface IPenaltyRepository : IGenericRepository<Penalty>
    {
    //    Task<Penalty> AddAsync(Penalty penalty);

    //    Task<Penalty?> GetByIdAsync(int id);

    //    Task<Penalty> UpdateAsync(Penalty penalty);

    //    Task<List<Penalty>> GetAllAsync();

        Task<List<Penalty>> GetByVehicleIdAsync(int vehicleId);
    }
}
