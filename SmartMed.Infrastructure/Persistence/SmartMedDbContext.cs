using Microsoft.EntityFrameworkCore;
using SmartMed.Domain.Medications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMed.Infrastructure.Persistence
{
    public class SmartMedDbContext: DbContext
    {
        public SmartMedDbContext(DbContextOptions<SmartMedDbContext> options) : base(options)
        { }

        public DbSet<Medication> Medications { get; set; }
    }
}
