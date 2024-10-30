using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartMed.Application.Medications;
using SmartMed.Application.Medications.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMed.Application
{
    public static class Configurations
    {
        public static IServiceCollection ConfigureApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMedicationMapper, MedicationMapper>();
            services.AddScoped<IMedicationAppService, MedicationAppService>();
            
            return services;
        }
    }
}
