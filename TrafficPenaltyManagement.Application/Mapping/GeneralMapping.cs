using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Application.Dtos.PenaltyApprovalHistoryDtos;
using TrafficPenaltyManagement.Application.Dtos.PenaltyDtos;
using TrafficPenaltyManagement.Application.Dtos.VehicleDtos;
using TrafficPenaltyManagement.Domain.Entitites;

namespace TrafficPenaltyManagement.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<CreateVehicleDto, Vehicle>();

            CreateMap<Vehicle, VehicleDto>();

            CreateMap<CreatePenaltyDto, Penalty>();
            

            CreateMap<Penalty, PenaltyDto>()
                .ForMember(
                    dest => dest.Plate,
                    opt => opt.MapFrom(src => src.Vehicle.Plate)
                );

            CreateMap<PenaltyApprovalHistory, PenaltyApprovalHistoryDto>();
        }
    }
}