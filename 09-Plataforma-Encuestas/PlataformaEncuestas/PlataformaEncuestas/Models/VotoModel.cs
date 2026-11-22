namespace PlataformaEncuestas.Models;

public class VotoModel
{
    public int Id { get; set; }

    public int EncuestaId { get; set; }

    public EncuestaModel? Encuesta { get; set; }

    public int OpcionId { get; set; }

    public OpcionModel? Opcion { get; set; }

    public string IdentificadorVotante { get; set; } = string.Empty;

    public DateTime FechaVoto { get; set; } = DateTime.Now;
}