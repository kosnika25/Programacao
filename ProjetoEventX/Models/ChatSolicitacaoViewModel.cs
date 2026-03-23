namespace ProjetoEventX.Models
{
    public class ChatSolicitacaoViewModel
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public AutorFeedViewModel Autor { get; set; }
        public DateTime DataEnvio { get; set; }
        public List<MensagemSolicitacao> Mensagens { get; set; } = new();
        public int UsuarioAtualId { get; set; }
    }
}
