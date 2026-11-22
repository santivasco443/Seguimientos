
using System.ComponentModel.DataAnnotations;

namespace PlataformaRecetas.Models
{
    public class RecetaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la receta es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(300)]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecciona una categoría.")]
        public string Categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los ingredientes son obligatorios.")]
        [StringLength(2000)]
        public string Ingredientes { get; set; } = string.Empty;

        [Required(ErrorMessage = "Las instrucciones son obligatorias.")]
        [StringLength(3000)]
        public string Instrucciones { get; set; } = string.Empty;

        [Range(1, 1440, ErrorMessage = "El tiempo debe estar entre 1 y 1440 minutos.")]
        public int TiempoPreparacion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
