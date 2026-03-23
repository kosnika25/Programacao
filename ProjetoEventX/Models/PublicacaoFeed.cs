using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ProjetoEventX.Models
{
    public class PublicacaoFeed
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventoId { get; set; }
        [ForeignKey("EventoId")]
            public required Evento Evento { get; set; }

        [Required]
        public int AutorId { get; set; }
        [ForeignKey("AutorId")]
            public required Organizador Autor { get; set; }

        [Required]
        [StringLength(1000)]
            public required string Texto { get; set; }

            public string? ImagemUrl { get; set; }

        public DateTime DataHora { get; set; } = DateTime.Now;

        public bool Fixado { get; set; } = false;

            public required ICollection<ComentarioFeed> Comentarios { get; set; }
    }
}
