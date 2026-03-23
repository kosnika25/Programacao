namespace ProjetoEventX.Models
{
    public class PublicacaoFeedViewModel
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public bool Fixado { get; set; }
        public string? ImagemUrl { get; set; }
        public DateTime DataHora { get; set; }
        public AutorFeedViewModel Autor { get; set; }
        public List<ComentarioFeedViewModel> Comentarios { get; set; } = new();
    }
}
