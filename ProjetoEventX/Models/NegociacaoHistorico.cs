using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEventX.Models
{
    [Table("NegociacaoHistoricos")]
    public class NegociacaoHistorico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuoteId { get; set; }

        [ForeignKey("QuoteId")]
        public Quote? Quote { get; set; }

        public int Rodada { get; set; } = 1;

        [Required]
        [StringLength(50)]
        public string TipoAcao { get; set; } = string.Empty;
        // "Solicitacao", "Resposta", "ContraProposta", "NovaOferta", "Aceite", "Recusa", "Cancelamento"

        [Range(0, 10000000)]
        public decimal? Valor { get; set; }

        [StringLength(2000)]
        public string? Mensagem { get; set; }

        public int UsuarioId { get; set; }

        [Required]
        [StringLength(30)]
        public string TipoUsuario { get; set; } = string.Empty; // "Organizador" ou "Fornecedor"

        public DateTime DataAcao { get; set; } = DateTime.UtcNow;
    }
}
