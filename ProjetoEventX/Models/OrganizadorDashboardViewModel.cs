namespace ProjetoEventX.Models
{
    public class OrganizadorDashboardViewModel
    {
        public int OrganizadorId { get; set; }
        public string NomeOrganizador { get; set; }
        public int EventosCriados { get; set; }
        public string FotoPerfilUrl { get; set; }
        public Pessoa Pessoa { get; set; }
        public string UserName { get; set; }
        public EventoDashboard ProximoEvento { get; set; }
        public List<EventoDashboard> Eventos { get; set; } = new();
        public List<CardAtualizacao> CardsAtualizacoes { get; set; } = new();
        public List<string> Checklist { get; set; } = new();
        public List<AcaoRapidaDashboard> AcoesRapidas { get; set; } = new();
        public List<NotificacaoDashboard> Notificacoes { get; set; } = new();
        public ResumoEventoDashboard ResumoEvento { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
