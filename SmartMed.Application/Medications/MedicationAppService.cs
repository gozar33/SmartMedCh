using FluentValidation;
using SmartMed.Application.Medications.DTOs;
using SmartMed.Application.Medications.Mappers;
using SmartMed.Domain.Medications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMed.Application.Medications
{
    public class MedicationAppService : IMedicationAppService
    {
        private readonly IMedicationRepository _medicationRepository;
        private readonly IMedicationValidator _medicationValidator;
        private readonly IMedicationMapper _medicationMapper;

        public MedicationAppService(IMedicationRepository medicationRepository,
            IMedicationValidator medicationValidator,
            IMedicationMapper medicationMapper)
        {
            _medicationRepository = medicationRepository;
            _medicationValidator = medicationValidator;
            _medicationMapper = medicationMapper;
        }

        public async Task AddAsync(MedicationDto medicationDto)
        {
            var medication = _medicationMapper.ToDomain(medicationDto);

            var validationResult = _medicationValidator.Validate(medication);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _medicationRepository.AddAsync(medication);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _medicationRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<MedicationListDto>> GetAllAsync()
        {
            var medications = await _medicationRepository.GetAllAsync();
            return medications.Select(s => _medicationMapper.ToListDto(s)).ToList();
        }

        public async Task<MedicationDto?> GetByIdAsync(int id)
        {
            var medication = await _medicationRepository.GetByIdAsync(id);
            return _medicationMapper.ToDto(medication);
        }
    }
}
