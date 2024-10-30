using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMed.Domain.Medications
{
    public interface IMedicationRepository
    {
        Task<IReadOnlyCollection<Medication>> GetAllAsync();
        Task<Medication?> GetByIdAsync(int id);
        Task AddAsync(Medication medication);
        Task<bool> DeleteAsync(int id);
    }
}
