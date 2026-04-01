using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEventX.Models
{
    public class Fornecedor : IdentityUser<int>
    {
        // Perfil profissional
        [StringLength(100)]
        public string NomeNegocio { get; set; }

        [StringLength(1000)]
        public string? Descricao { get; set; }
        [StringLength(100)]
        public string? Categoria { get; set; }

        [StringLength(100)]
        public string? FaixaPreco { get; set; }

        [StringLength(255)]
        public string? ContatoComercial { get; set; }

        // Galeria/Portfólio
        public ICollection<FotoPortfolio> Portfolio { get; set; } = new List<FotoPortfolio>();

        // Serviços oferecidos
        public ICollection<ServicoFornecedor> Servicos { get; set; } = new List<ServicoFornecedor>();

        // Disponibilidade (ex: string json, ou model separado)
        [StringLength(500)]
        public string? Disponibilidade { get; set; }
        [Required]
        public int PessoaId { get; set; }

        [ForeignKey("PessoaId")]
        public required Pessoa Pessoa { get; set; }

        [Required]
        [StringLength(18)]
        public required string Cnpj { get; set; }

        [StringLength(255)]
        public required string TipoServico { get; set; }

        [Required]
        [StringLength(100)]
        public required string Cidade { get; set; }

        [Required]
        [StringLength(2)]
        public required string UF { get; set; }
        // ------------------------------------

        public decimal AvaliacaoMedia { get; set; } = 0.0m;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Relacionamentos
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public ICollection<AvaliacaoFornecedor> Avaliacoes { get; set; } = new List<AvaliacaoFornecedor>();
        public ICollection<SolicitacaoOrcamento> SolicitacoesRecebidas { get; set; } = new List<SolicitacaoOrcamento>();

        // Propriedades adicionadas
        public string? FotoPerfilUrl { get; set; }
        public string? Regiao { get; set; }
        public string? Telefone { get; set; }
        public List<string> ServicosOferecidos { get; set; } = new List<string>();
        public List<string> Galeria { get; set; } = new List<string>();
        public int TotalAvaliacoes { get; set; }
    }
}