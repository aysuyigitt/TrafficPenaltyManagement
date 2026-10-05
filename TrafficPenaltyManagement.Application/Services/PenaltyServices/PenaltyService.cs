using AutoMapper;
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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFileService _fileService;

        public PenaltyService(IPenaltyRepository penaltyRepository, IMapper mapper, IPenaltyApprovalHistoryRepository historyRepository, IVehicleRepository vehicleRepository, IEmployeeRepository employeeRepository, IFileService fileService)
        {
            _penaltyRepository = penaltyRepository;
            _mapper = mapper;
            _historyRepository = historyRepository;
            _vehicleRepository = vehicleRepository;
            _employeeRepository = employeeRepository;
            _fileService = fileService;
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

        public async Task<PenaltyDto> CreatePenaltyAsync(CreatePenaltyDto createPenaltyDto)
        {
            var vehicle = await _vehicleRepository.GetByPlateAsync(createPenaltyDto.Plate);

            if (vehicle == null)
            {
                throw new InvalidOperationException("Bu plakaya ait araç bulunamadı.");
            }

            var employee = await _employeeRepository.GetByIdAsync(createPenaltyDto.EmployeeId);

            if (employee == null)
            {
                throw new InvalidOperationException("Çalışan bulunamadı.");
            }

            var assignment = employee.VehicleAssignments?
                .FirstOrDefault(x =>
                    x.VehicleId == vehicle.Id &&
                    x.StartDate.Date <= createPenaltyDto.PenaltyDate.Date &&
                    (x.EndDate == null || x.EndDate.Value.Date >= createPenaltyDto.PenaltyDate.Date));

            if (assignment == null)
            {
                throw new InvalidOperationException(
                    "Seçilen çalışan, ceza tarihinde bu araca atanmış değildir.");
            }

            string? documentPath = null;

            if (createPenaltyDto.Document != null)
            {
                documentPath = await _fileService.SaveFileAsync(
                    createPenaltyDto.Document,
                    "penalties");
            }

            var penalty = new Penalty
            {
                VehicleId = vehicle.Id,
                PenaltyTypeId = createPenaltyDto.PenaltyTypeId,
                Amount = createPenaltyDto.Amount,
                PenaltyDate = createPenaltyDto.PenaltyDate,
                DocumentPath = documentPath,
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

        public async Task<PenaltyDto?> GetPenaltyByIdAsync(int penaltyId)
        {
            var penalty = await _penaltyRepository.GetByIdAsync(penaltyId);

            return _mapper.Map<PenaltyDto>(penalty);
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

        public async Task UpdatePenaltyAsync(UpdatePenaltyDto updatePenaltyDto)
        {
            var penalty = await _penaltyRepository.GetByIdAsync(updatePenaltyDto.Id);

            if (penalty == null)
            {
                throw new InvalidOperationException("Güncellenecek ceza bulunamadı.");
            }

            penalty.PenaltyTypeId = updatePenaltyDto.PenaltyTypeId;
            penalty.PenaltyDate = updatePenaltyDto.PenaltyDate;

            if (updatePenaltyDto.Document != null)
            {
                var documentPath = await _fileService.SaveFileAsync(
                    updatePenaltyDto.Document,
                    "penalties");

                penalty.DocumentPath = documentPath;
            }

            await _penaltyRepository.UpdateAsync(penalty);
        }
    }
        }
