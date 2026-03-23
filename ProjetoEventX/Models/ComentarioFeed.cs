using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEventX.Models
{
    public class ComentarioFeed
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PublicacaoFeedId { get; set; }
            [ForeignKey("PublicacaoFeedId")]
                public required PublicacaoFeed PublicacaoFeed { get; set; }

        [Required]
        public int AutorId { get; set; }
        [ForeignKey("AutorId")]
                public required Convidado Autor { get; set; }

        [Required]
        [StringLength(500)]
                public required string Texto { get; set; }

        public DateTime DataHora { get; set; } = DateTime.Now;
    }
}
