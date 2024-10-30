using SmartMed.Application.Medications.DTOs;
using SmartMed.Domain.Medications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMed.Application.Medications.Mappers
{
    public class MedicationMapper : IMedicationMapper
    {
        public Medication ToDomain(MedicationDto medicationDto)
        {
            if (medicationDto == null) return null;

            return new Medication
            {
                Quantity = medicationDto.Quantity,
                CreatedAt = DateTime.UtcNow,
                Name = medicationDto.Name,
            };
        }

        public MedicationDto ToDto(Medication medication)
        {
            if (medication == null) return null;

            return new MedicationDto
            {
                Quantity = medication.Quantity,
                Name = medication.Name,
            };
        }

        public MedicationListDto ToListDto(Medication medication)
        {
            if (medication == null) return null;

            return new MedicationListDto
            {
                Quantity = medication.Quantity,
                Name = medication.Name,
                Id = medication.Id,
                CreatedAt = medication.CreatedAt,
            };
        }
    }
}
