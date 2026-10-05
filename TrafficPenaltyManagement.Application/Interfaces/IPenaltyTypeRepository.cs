using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Domain.Entitites;

namespace TrafficPenaltyManagement.Application.Interfaces
{
    public interface IPenaltyTypeRepository: IGenericRepository<PenaltyType>
    {
        //Task<PenaltyType> AddAsync(PenaltyType penaltyType);
        //Task<List<PenaltyType>> GetAllAsync();
        //Task<PenaltyType?> GetByIdAsync(int id);
        //Task UpdateAsync(PenaltyType penaltyType);
        //Task DeleteAsync(PenaltyType penaltyType);
    }
}
