using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEventX.Models
{
    [Table("Notifications")]
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Message { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;
        // "RespostaOrcamento", "NovaMensagem", "PropostaAceita", "PropostaRecusada",
        // "NovoPedido", "PagamentoConfirmado", "ConfirmacaoPresenca"

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [StringLength(500)]
        public string? Link { get; set; }
    }
}
