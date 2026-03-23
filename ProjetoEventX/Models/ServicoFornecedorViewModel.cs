namespace ProjetoEventX.Models
{
    public class ServicoFornecedorViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal? Preco { get; set; }
        public bool Ativo { get; set; }
        public string DescricaoCurta { get; set; }
        public string FaixaPreco { get; set; }
    }
}
