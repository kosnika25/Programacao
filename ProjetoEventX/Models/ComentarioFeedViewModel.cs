namespace ProjetoEventX.Models
{
    public class ComentarioFeedViewModel
    {
        public int Id { get; set; }
        public required string Texto { get; set; }
        public DateTime DataHora { get; set; }
        public required AutorFeedViewModel Autor { get; set; }
    }
}
