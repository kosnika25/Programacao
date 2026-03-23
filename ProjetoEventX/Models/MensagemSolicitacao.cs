using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEventX.Models
{
    public class MensagemSolicitacao
    {
        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime DataEnvio { get; set; }
        [Key]
        public int Id { get; set; }

        [Required]
        public int SolicitacaoOrcamentoId { get; set; }
        [ForeignKey("SolicitacaoOrcamentoId")]
        public required SolicitacaoOrcamento SolicitacaoOrcamento { get; set; }

        [Required]
        public int AutorId { get; set; }
        [Required]
        public required string AutorTipo { get; set; } // "Organizador" ou "Fornecedor"

        [Required]
        [StringLength(2000)]
        public required string Texto { get; set; }

        public DateTime DataHora { get; set; } = DateTime.Now;
    }
}
