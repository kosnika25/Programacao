using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEventX.Models
{
    public class FornecedorFavorito
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrganizadorId { get; set; }
        [ForeignKey("OrganizadorId")]
        public Organizador Organizador { get; set; }

        [Required]
        public int FornecedorId { get; set; }
        [ForeignKey("FornecedorId")]
        public Fornecedor Fornecedor { get; set; }

        public DateTime DataFavorito { get; set; } = DateTime.Now;
    }
}
