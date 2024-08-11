using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonEmissions.Shared.Entities
{
    public class Company
    {
        public int Id { get; set; }

        [Display(Name = "Nombre de la compañía")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [MinLength(12, ErrorMessage = "El campo {0} no puede tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string CompanyName { get; set; } = null!;
        public ICollection<Emission>? Emissions { get; set; }
                
        public decimal CalculateCCF(DateTime startDate, DateTime endDate)
        {
            if (Emissions == null)
                return 0;

            return Emissions
                .Where(e => e.EmissionDate >= startDate && e.EmissionDate <= endDate)
                .Sum(e => e.Quantity);
        }
    }
}