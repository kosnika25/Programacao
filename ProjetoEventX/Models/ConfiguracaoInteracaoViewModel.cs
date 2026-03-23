namespace ProjetoEventX.Models
{
    public class ConfiguracaoInteracaoViewModel
    {
        public int Id { get; set; }
        public string NomeConfiguracao { get; set; }
        public string Valor { get; set; }
        public bool PermitirComentarios { get; set; }
        public bool PermitirEnvioFotos { get; set; }
        public bool PermitirCurtidas { get; set; }
        public bool ExigirAprovacaoFotos { get; set; }
        public bool MuralVisivel { get; set; }
        public bool PermitirInteracoes { get; set; }
    }
}
