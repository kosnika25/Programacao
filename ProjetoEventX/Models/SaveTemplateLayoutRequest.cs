using System.ComponentModel.DataAnnotations;

namespace ProjetoEventX.Models
{
    public class SaveTemplateLayoutRequest
    {
        [Required]
        public int TemplateId { get; set; }

        [Required]
        public string LayoutJson { get; set; } = string.Empty;
    }
}