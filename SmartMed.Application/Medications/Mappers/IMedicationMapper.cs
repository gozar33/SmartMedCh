using SmartMed.Application.Medications.DTOs;
using SmartMed.Domain.Medications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMed.Application.Medications.Mappers
{
    public interface IMedicationMapper
    {
        MedicationDto ToDto(Medication medication);
        Medication ToDomain(MedicationDto medication);
        MedicationListDto ToListDto(Medication medication);
    }
}
