namespace CronometroOnline.Models
{
    public class CronometroModel
    {
        public string Titulo { get; set; } = "Cronómetro Online";

        public string Descripcion { get; set; } =
            "Controla tu tiempo y registra tus vueltas.";

        public int TiempoInicial { get; set; } = 0;

        public string FormatoTiempo { get; set; } =
            "HH:MM:SS.MS";
    }
}