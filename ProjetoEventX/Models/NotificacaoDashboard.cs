namespace ProjetoEventX.Models
{
    public class NotificacaoDashboard
    {
        public required string Mensagem { get; set; }
        public DateTime Data { get; set; }
        public required string Tipo { get; set; }
    }
}
