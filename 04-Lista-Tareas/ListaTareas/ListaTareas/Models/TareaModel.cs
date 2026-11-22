using System.ComponentModel.DataAnnotations;

namespace ListaTareas.Models
{
    public class TareaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Escribe el nombre de la tarea.")]
        [StringLength(100, ErrorMessage = "La tarea no puede superar los 100 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;

        public bool Completada { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}