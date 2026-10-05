using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Application.Interfaces;
using TrafficPenaltyManagement.Application.Dtos.DashboardDtos;

namespace TrafficPenaltyManagement.Application.Services.DashboardService
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repository;

        public DashboardService(IDashboardRepository repository)
        {
            _repository = repository;
        }

        public Task<DashboardDto> GetDashboardDataAsync()
        {
            var dashboard = _repository.GetDashboardDataAsync();
            return dashboard;
        }
    }
}
