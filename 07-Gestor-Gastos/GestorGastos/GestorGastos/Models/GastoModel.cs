
using System.ComponentModel.DataAnnotations;

namespace GestorGastos.Models
{
    public class GastoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(100, ErrorMessage = "La descripción no puede superar los 100 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecciona una categoría.")]
        public string Categoria { get; set; } = string.Empty;

        [Range(0.01, 999999999, ErrorMessage = "Ingresa un monto válido.")]
        public decimal Monto { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Today;
    }
}
