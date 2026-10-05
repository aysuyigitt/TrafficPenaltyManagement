using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Application.Dtos.DashboardDtos;

namespace TrafficPenaltyManagement.Application.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardDto> GetDashboardDataAsync();


    }
}
