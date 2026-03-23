namespace ProjetoEventX.Models
{
    public class UsuarioPerfilViewModel
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string FotoPerfilUrl { get; set; }
        public string FotoUrl { get; set; }
        public string Telefone { get; set; }
        public List<string> Preferencias { get; set; } = new();
    }
}
