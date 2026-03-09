using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEventX.Models
{
    [Table("FornecedorRankings")]
    public class FornecedorRanking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FornecedorId { get; set; }

        [ForeignKey("FornecedorId")]
        public Fornecedor? Fornecedor { get; set; }

        /// <summary>Média das avaliações (1–5)</summary>
        [Range(0, 5)]
        public decimal MediaAvaliacoes { get; set; }

        /// <summary>Quantidade de avaliações recebidas</summary>
        public int QuantidadeAvaliacoes { get; set; }

        /// <summary>Quantidade de pedidos concluídos (Pago ou Entregue)</summary>
        public int PedidosConcluidos { get; set; }

        /// <summary>Tempo médio de resposta em horas para orçamentos</summary>
        public decimal TempoMedioRespostaHoras { get; set; }

        /// <summary>Taxa de aceitação de orçamentos (%)</summary>
        [Range(0, 100)]
        public decimal TaxaAceitacao { get; set; }

        /// <summary>Taxa de cancelamento de orçamentos (%)</summary>
        [Range(0, 100)]
        public decimal TaxaCancelamento { get; set; }

        /// <summary>Quantidade de eventos distintos atendidos</summary>
        public int EventosAtendidos { get; set; }

        /// <summary>Pontuação geral calculada (0–100)</summary>
        [Range(0, 100)]
        public decimal PontuacaoGeral { get; set; }

        /// <summary>Posição no ranking geral</summary>
        public int PosicaoRanking { get; set; }

        /// <summary>Se o fornecedor possui o badge "Top Fornecedor"</summary>
        public bool IsTopFornecedor { get; set; }

        public DateTime UltimaAtualizacao { get; set; } = DateTime.UtcNow;
    }
}
