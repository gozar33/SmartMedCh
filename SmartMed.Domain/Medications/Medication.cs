using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMed.Domain.Medications
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is required.")]
        [StringLength(1000, ErrorMessage = "The Medication Name cannot be longer than 1000 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Quantity is required.")]
        public int Quantity { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}
