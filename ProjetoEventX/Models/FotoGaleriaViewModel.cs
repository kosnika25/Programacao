namespace ProjetoEventX.Models
{
    public class FotoGaleriaViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHora { get; set; }
        public string Legenda { get; set; }
        public string ImagemUrl { get; set; }
        public AutorFeedViewModel Autor { get; set; }
        public List<ComentarioFeedViewModel> Comentarios { get; set; } = new();
    }
}
