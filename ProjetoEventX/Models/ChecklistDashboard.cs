namespace ProjetoEventX.Models
{
    public class ChecklistDashboard
    {
        public required string NomeEvento { get; set; }
        public DateTime DataEvento { get; set; }
        public required string StatusEvento { get; set; }
    }
}
