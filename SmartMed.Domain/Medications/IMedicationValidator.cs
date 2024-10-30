using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMed.Domain.Medications
{
    public interface IMedicationValidator
    {
        ValidationResult Validate(Medication medication);
    }
}
