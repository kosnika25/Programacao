using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEventX.Models
{
    public class TemplateConvite
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        public string? Estilo { get; set; }
        public bool Ativo { get; set; } = true;

        public string? CorFundo { get; set; }
        public string? CorTexto { get; set; }
        public string? CorPrimaria { get; set; }
        public string? Fonte { get; set; }

        public string? Titulo { get; set; }
        public string? Saudacao { get; set; }
        public string? Mensagem { get; set; }
        public string? TextoBotao { get; set; }

        public int? EventoId { get; set; }
        public virtual Evento? Evento { get; set; }

        public string? LayoutJson { get; set; }
    }
}