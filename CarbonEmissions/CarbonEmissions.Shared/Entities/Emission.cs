using CarbonEmissions.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarbonEmissions.Shared.Entities
{
    public class Emission
    {
        public int Id { get; set; }

        [Display(Name = "Descripción de la Emisión")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [MinLength(20, ErrorMessage = "El campo {0} no puede tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "La cantidad no puede ser menor a cero.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Quantity {  get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime EmissionDate { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public EmissionType EmissionType { get; set; } 

        public Company? Company { get; set; }
    }
}