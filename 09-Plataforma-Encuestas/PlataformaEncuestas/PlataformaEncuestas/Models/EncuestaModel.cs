using System.ComponentModel.DataAnnotations;

namespace PlataformaEncuestas.Models;

public class EncuestaModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Titulo { get; set; } = string.Empty;

    [StringLength(300)]
    public string Descripcion { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Pregunta { get; set; } = string.Empty;

    public bool Publicada { get; set; } = true;

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public List<OpcionModel> Opciones { get; set; } = new();

    public List<VotoModel> Votos { get; set; } = new();
}