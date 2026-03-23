using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ProjetoEventX.Models
{
    public class FotoGaleria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventoId { get; set; }
        [ForeignKey("EventoId")]
        public Evento Evento { get; set; }

        [Required]
        public int AutorId { get; set; }
        [ForeignKey("AutorId")]
        public Convidado Autor { get; set; }

        [Required]
        public string ImagemUrl { get; set; }

        [StringLength(200)]
        public string Legenda { get; set; }

        public DateTime DataHora { get; set; } = DateTime.Now;

        public bool Aprovada { get; set; } = false;

        public ICollection<ComentarioGaleria> Comentarios { get; set; } = new List<ComentarioGaleria>();
    }
}
