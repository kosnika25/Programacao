using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEventX.Models
{
    public class ComentarioGaleria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FotoId { get; set; }
        [ForeignKey("FotoId")]
        public FotoGaleria Foto { get; set; }

        [Required]
        public int AutorId { get; set; }
        [ForeignKey("AutorId")]
        public Convidado Autor { get; set; }

        [Required]
        [StringLength(500)]
        public string Texto { get; set; }

        public DateTime DataHora { get; set; } = DateTime.Now;
    }
}
