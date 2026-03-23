namespace ProjetoEventX.Models
{
    public class ConvidadoAcessoViewModel
    {
        public int ConvidadoId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool TemAcesso { get; set; }
        public bool AcessoGerado { get; set; }
        public bool PrimeiroAcessoConcluido { get; set; }
        public bool Ativo { get; set; }
        public string EventoNome { get; set; }
        public DateTime UltimoAcesso { get; set; }
        public int Id { get; set; }
    }
}
