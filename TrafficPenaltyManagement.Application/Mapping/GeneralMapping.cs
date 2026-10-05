
using AutoMapper;
using TrafficPenaltyManagement.Application.Dtos.EmployeeDtos;
using TrafficPenaltyManagement.Application.Dtos.PenaltyApprovalHistoryDtos;
using TrafficPenaltyManagement.Application.Dtos.PenaltyDtos;
using TrafficPenaltyManagement.Application.Dtos.VehicleAssignmentDtos;
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
                )
                .ForMember(
                    dest => dest.EmployeeName,
                    opt => opt.MapFrom(src =>
                        src.Vehicle.VehicleAssignments
                            .Where(x => x.EndDate == null)
                            .Select(x => x.Employee.FirstName + " " + x.Employee.LastName)
                            .FirstOrDefault()
                    )
                )
               .ForMember(
    dest => dest.EmployeeName,
    opt => opt.MapFrom(src =>
        src.Vehicle.VehicleAssignments
            .Where(x =>
                src.PenaltyDate >= x.StartDate &&
                (
                    x.EndDate == null ||
                    src.PenaltyDate <= x.EndDate.Value
                )
            )
            .Select(x => x.Employee.FirstName + " " + x.Employee.LastName)
            .FirstOrDefault()
    )
);

            CreateMap<UpdatePenaltyDto, Penalty>();

            CreateMap<PenaltyApprovalHistory, PenaltyApprovalHistoryDto>();

            CreateMap<CreateEmployeeDto, Employee>();

            CreateMap<Employee, EmployeeDto>();

            CreateMap<EmployeeDto, Employee>();

            CreateMap<UpdateEmployeeDto, Employee>();

            CreateMap<VehicleAssignment, VehicleAssignmentDto>();

            CreateMap<PenaltyType, PenaltyTypeDto>();
            CreateMap<PenaltyTypeDto, PenaltyType>();
            CreateMap<CreatePenaltyTypeDto, PenaltyType>();
            CreateMap<UpdatePenaltyTypeDto, PenaltyType>();
        }
    }
}