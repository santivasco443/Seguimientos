using System.ComponentModel.DataAnnotations;

namespace CalculadoraPropinas.Models
{
    public class CalculadoraModel
    {
        [Required(ErrorMessage = "Debes ingresar el monto de la cuenta.")]
        [Range(0.01, 100000000, ErrorMessage = "El monto debe ser mayor que cero.")]
        [Display(Name = "Monto de la cuenta")]
        public decimal MontoCuenta { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un porcentaje.")]
        [Range(0, 100, ErrorMessage = "La propina debe estar entre 0% y 100%.")]
        [Display(Name = "Porcentaje de propina")]
        public decimal PorcentajePropina { get; set; } = 15;

        public decimal CalcularPropina()
        {
            return MontoCuenta * (PorcentajePropina / 100);
        }

        public decimal CalcularTotal()
        {
            return MontoCuenta + CalcularPropina();
        }
    }
}