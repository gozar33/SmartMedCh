using Microsoft.EntityFrameworkCore;
using SmartMed.Domain.Medications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMed.Infrastructure.Persistence.Repositories
{
    public class MedicationRepository : IMedicationRepository
    {
        private readonly SmartMedDbContext _smartMedDbContext;
        public MedicationRepository(SmartMedDbContext smartMedDbContext)
        {
            _smartMedDbContext = smartMedDbContext;
        }
        public async Task AddAsync(Medication medication)
        {
            await _smartMedDbContext.Medications.AddAsync(medication);
            await _smartMedDbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var medication = await GetByIdAsync(id);
            if (medication == null) return false;

            _smartMedDbContext.Medications.Remove(medication);
            await _smartMedDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyCollection<Medication>> GetAllAsync()
        {
            return await _smartMedDbContext.Medications.AsNoTracking().ToListAsync();
        }

        public async Task<Medication?> GetByIdAsync(int id)
        {
            return  await _smartMedDbContext.Medications.FindAsync(id);
        }
    }
}
