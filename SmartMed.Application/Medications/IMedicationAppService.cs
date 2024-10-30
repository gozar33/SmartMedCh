using SmartMed.Application.Medications.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMed.Application.Medications
{
    public interface IMedicationAppService
    {
        Task<IEnumerable<MedicationListDto>> GetAllAsync();
        Task<MedicationDto?> GetByIdAsync(int id);
        Task AddAsync(MedicationDto medication);
        Task<bool> DeleteAsync(int id);
    }
}
