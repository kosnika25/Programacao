namespace ProjetoEventX.Models
{
    public class ResumoEventoDashboard
    {
        public required string NomeEvento { get; set; }
        public required string EmailOrganizador { get; set; }
        public int TotalConvidados { get; set; }
        public int TotalDespesas { get; set; }
    }
}
