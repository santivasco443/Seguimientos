
using System.ComponentModel.DataAnnotations;

namespace GestorNotas.Models
{
    public class NotaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(80, ErrorMessage = "El título no puede superar los 80 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El contenido es obligatorio.")]
        [StringLength(2000, ErrorMessage = "La nota no puede superar los 2000 caracteres.")]
        public string Contenido { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}
