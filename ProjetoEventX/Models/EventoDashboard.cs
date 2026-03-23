namespace ProjetoEventX.Models
{
    public class EventoDashboard
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string ImagemCapaUrl { get; set; }
        public DateTime DataEvento { get; set; }
        public required string HoraInicio { get; set; }
        public required string Local { get; set; }
        public required string StatusEvento { get; set; }
        public required string NomeEvento { get; set; }
        public required string TipoEvento { get; set; }
        public required string HoraFim { get; set; }
    }
}
