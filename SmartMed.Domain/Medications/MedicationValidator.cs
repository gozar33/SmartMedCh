using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMed.Domain.Medications
{
    public class MedicationValidator: AbstractValidator<Medication>, IMedicationValidator
    {
        public MedicationValidator()
        {
            RuleFor(medication => medication.Name)
           .NotNull().WithMessage("The Name field is required.");

            RuleFor(medication => medication.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero");
        }
    }
}
