using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPenaltyManagement.Application.Dtos.PenaltyDtos;
using TrafficPenaltyManagement.Application.Interfaces;
using TrafficPenaltyManagement.Domain.Entitites;

namespace TrafficPenaltyManagement.Application.Services.PenaltyTypeServices
{
    public class PenaltyTypeService : IPenaltyTypeService
    {
        private readonly IPenaltyTypeRepository _penaltyTypeRepository;
        private readonly IMapper _mapper;

        public PenaltyTypeService(IPenaltyTypeRepository penaltyTypeRepository, IMapper mapper)
        {
            _penaltyTypeRepository = penaltyTypeRepository;
            _mapper = mapper;
        }

        public async Task<PenaltyTypeDto> CreatePenaltyTypeAsync(CreatePenaltyTypeDto createPenaltyTypeDto)
        {
            var penaltyType = _mapper.Map<PenaltyType>(createPenaltyTypeDto);

            var createdPenaltyType = await _penaltyTypeRepository.AddAsync(penaltyType);

            return _mapper.Map<PenaltyTypeDto>(createdPenaltyType);

        }

        public async Task DeletePenaltyTypeAsync(int id)
        {
            var penaltyType = await _penaltyTypeRepository.GetByIdAsync(id);

            if (penaltyType == null)
            {
                throw new Exception("Ceza türü bulunamadı.");
            }

            await _penaltyTypeRepository.DeleteAsync(penaltyType);

        }

        public async Task<List<PenaltyTypeDto>> GetAllPenaltyTypeAsync()
        {
            var penaltyTypes = await _penaltyTypeRepository.GetAllAsync();

            return _mapper.Map<List<PenaltyTypeDto>>(penaltyTypes);

        }

        public async Task<PenaltyTypeDto?> GetByIdAsync(int id)
        {
            var penaltyType = await _penaltyTypeRepository.GetByIdAsync(id);

            if (penaltyType == null)
            {
                return null;
            }

            return _mapper.Map<PenaltyTypeDto>(penaltyType);
        }

        public async Task UpdatePenaltyTypeAsync(UpdatePenaltyTypeDto updatePenaltyTypeDto)
        {
            var penaltyType = _mapper.Map<PenaltyType>(updatePenaltyTypeDto);

            await _penaltyTypeRepository.UpdateAsync(penaltyType);
            ;
        }
    }
}