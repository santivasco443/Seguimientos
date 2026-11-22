using System.ComponentModel.DataAnnotations;

namespace PlataformaEncuestas.Models;

public class OpcionModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Texto { get; set; } = string.Empty;

    public int EncuestaId { get; set; }

    public EncuestaModel? Encuesta { get; set; }

    public List<VotoModel> Votos { get; set; } = new();
}