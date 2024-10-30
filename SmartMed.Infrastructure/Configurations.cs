using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartMed.Domain.Medications;
using SmartMed.Infrastructure.Persistence;
using SmartMed.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMed.Infrastructure
{
    public static class Configurations
    {
        public static IServiceCollection ConfigureInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SmartMedDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient<IMedicationValidator, MedicationValidator>();
            services.AddScoped<IMedicationRepository, MedicationRepository>();

            return services;
        }
    }
}
