using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEventX.Models
{
    [Table("EventLogs")]
    public class EventLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public Evento? Evento { get; set; }

        public int? UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string ActionType { get; set; } = string.Empty;
        // "SolicitacaoOrcamento", "RespostaFornecedor", "MensagemChat",
        // "PropostaAceita", "PropostaRecusada", "PedidoCriado",
        // "PagamentoConfirmado", "DespesaRegistrada", "ConviteEnviado",
        // "ConfirmacaoPresenca"

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
