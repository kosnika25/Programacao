namespace ProjetoEventX.Models
{
    public class RespostaPropostaViewModel
    {
        public int Id { get; set; }
        public string Resposta { get; set; }
        public DateTime DataResposta { get; set; }
        public string NomeFornecedor { get; set; }
        public string EmailFornecedor { get; set; }
        public string Mensagem { get; set; }
    }
}
