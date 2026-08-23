using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Application.Dtos.PenaltyApprovalHistoryDtos;
using TrafficPenaltyManagement.Application.Dtos.PenaltyDtos;
using TrafficPenaltyManagement.Application.Interfaces;
using TrafficPenaltyManagement.Domain.Entitites;
using TrafficPenaltyManagement.Domain.Enums;

namespace TrafficPenaltyManagement.Application.Services.PenaltyServices
{
    public class PenaltyService : IPenaltyService
    {
        private readonly IPenaltyRepository _penaltyRepository;
        private readonly IMapper _mapper;
        private readonly IPenaltyApprovalHistoryRepository _historyRepository;
        private readonly IVehicleRepository _vehicleRepository;


        public PenaltyService(IPenaltyRepository penaltyRepository, IMapper mapper, IPenaltyApprovalHistoryRepository historyRepository, IVehicleRepository vehicleRepository)
        {
            _penaltyRepository = penaltyRepository;
            _mapper = mapper;
            _historyRepository = historyRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<PenaltyDto> ApprovePenaltyAsync(int penaltyId, string userId, UserRole userRole)
        {
            var penalty = await _penaltyRepository.GetByIdAsync(penaltyId);

            if (penalty == null)
            {
                throw new InvalidOperationException("Onaylanacak ceza bulunamadı.");
            }

            var previousStatus = penalty.Status;

            if (penalty.Status == PenaltyStatus.New)
            {
                if (userRole != UserRole.Manager)
                {
                    throw new InvalidOperationException("Bu aşamada işlem yapma yetkiniz bulunmamaktadır.");
                }

                penalty.Status = PenaltyStatus.ManagerApproval;
            }
            else if (penalty.Status == PenaltyStatus.ManagerApproval)
            {
                if (userRole != UserRole.Finance)
                {
                    throw new InvalidOperationException("Bu aşamada işlem yapma yetkiniz bulunmamaktadır.");
                }

                penalty.Status = PenaltyStatus.FinanceApproval;
            }
            else if (penalty.Status == PenaltyStatus.FinanceApproval)
            {
                if (userRole != UserRole.Finance)
                {
                    throw new InvalidOperationException("Bu aşamada işlem yapma yetkiniz bulunmamaktadır.");
                }

                penalty.Status = PenaltyStatus.Completed;
            }
            else
            {
                throw new InvalidOperationException("Bu ceza mevcut durumunda onaylanamaz.");
            }

            await _penaltyRepository.UpdateAsync(penalty);

            var history = new PenaltyApprovalHistory
            {
                PenaltyId = penalty.Id,
                UserId = userId,
                ActionDate = DateTime.Now,
                ActionType = ApprovalActionType.Approve,
                PreviousStatus = previousStatus,
                NewStatus = penalty.Status
            };

            await _historyRepository.AddAsync(history);

            return _mapper.Map<PenaltyDto>(penalty);
        }

        public async Task<PenaltyDto> CreatePenaltyAsync(
            CreatePenaltyDto createPenaltyDto)
        {
            var vehicle = await _vehicleRepository.GetByPlateAsync(createPenaltyDto.Plate);

            if (vehicle == null)
            {
                throw new InvalidOperationException("Bu plakaya ait araç bulunamadı.");
            }

            var penalty = new Penalty
            {
                VehicleId = vehicle.Id,
                Status = PenaltyStatus.New
            };

            var createdPenalty = await _penaltyRepository.AddAsync(penalty);

            return _mapper.Map<PenaltyDto>(createdPenalty);
        }

        public async Task<List<PenaltyDto>> GetAllPenaltiesAsync()
        {
            var penalties = await _penaltyRepository.GetAllAsync();

            return _mapper.Map<List<PenaltyDto>>(penalties);
        }

        public async Task<List<PenaltyApprovalHistoryDto>> GetPenaltyHistoryAsync(
            int penaltyId)
        {
            var penalty = await _penaltyRepository.GetByIdAsync(penaltyId);

            if (penalty == null){
                throw new InvalidOperationException("Ceza bulunamadı.");
            }

            var histories = await _historyRepository.GetByPenaltyIdAsync(penaltyId);

            return _mapper.Map<List<PenaltyApprovalHistoryDto>>(histories);
        }

        public async Task<PenaltyDto> RejectPenaltyAsync(int penaltyId,string rejectionReason,string userId,UserRole userRole)
        {
            var penalty = await _penaltyRepository.GetByIdAsync(penaltyId);

            if (penalty == null){
                throw new InvalidOperationException("Reddedilecek ceza bulunamadı.");
            }

            if (penalty.Status != PenaltyStatus.New && penalty.Status != PenaltyStatus.ManagerApproval &&
                penalty.Status != PenaltyStatus.FinanceApproval)
            {
                throw new InvalidOperationException("Bu ceza mevcut durumunda reddedilemez.");
            }

            if (string.IsNullOrWhiteSpace(rejectionReason))
            {
                throw new InvalidOperationException("Ret nedeni belirtilmelidir.");
            }

            var previousStatus = penalty.Status;

            if (penalty.Status == PenaltyStatus.New && userRole != UserRole.Manager)
            {
                throw new InvalidOperationException("Bu aşamada işlem yapma yetkiniz bulunmamaktadır.");
            }

            if (penalty.Status == PenaltyStatus.ManagerApproval &&userRole != UserRole.Finance)
            {
                throw new InvalidOperationException("Bu aşamada işlem yapma yetkiniz bulunmamaktadır.");
            }

            
            if (penalty.Status == PenaltyStatus.FinanceApproval && userRole != UserRole.Finance)
            {
                throw new InvalidOperationException("Bu aşamada işlem yapma yetkiniz bulunmamaktadır.");
            }

            penalty.Status = PenaltyStatus.Rejected;
            penalty.RejectionReason = rejectionReason;

            await _penaltyRepository.UpdateAsync(penalty);

            var history = new PenaltyApprovalHistory
            {
                PenaltyId = penalty.Id,
                UserId = userId,
                ActionDate = DateTime.Now,
                ActionType = ApprovalActionType.Reject,
                Description = rejectionReason,
                PreviousStatus = previousStatus,
                NewStatus = penalty.Status
            };

            await _historyRepository.AddAsync(history);

            return _mapper.Map<PenaltyDto>(penalty);
        }
    }
}