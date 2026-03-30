namespace ProjetoEventX.Models
{
    public class EventoDashboard
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string ImagemCapaUrl { get; set; }
        public DateTime DataEvento { get; set; }
        public string HoraInicio { get; set; }
        public string Local { get; set; }
        public string StatusEvento { get; set; }
        public string NomeEvento { get; set; }
        public string TipoEvento { get; set; }
        public string HoraFim { get; set; }
    }
}
