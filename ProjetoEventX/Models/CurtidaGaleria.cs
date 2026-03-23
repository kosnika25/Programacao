using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEventX.Models
{
    public class CurtidaGaleria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FotoGaleriaId { get; set; }
        [ForeignKey("FotoGaleriaId")]
        public FotoGaleria FotoGaleria { get; set; }

        [Required]
        public int UsuarioId { get; set; }
        public string UsuarioTipo { get; set; } // "Convidado" ou "Organizador"

        public DateTime DataCurtida { get; set; } = DateTime.Now;
    }
}
