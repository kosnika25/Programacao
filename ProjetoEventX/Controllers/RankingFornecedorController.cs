using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoEventX.Data;
using ProjetoEventX.Models;
using ProjetoEventX.Services;

namespace ProjetoEventX.Controllers
{
    [Authorize]
    public class RankingFornecedorController : Controller
    {
        private readonly EventXContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FornecedorPerformanceService _performanceService;

        public RankingFornecedorController(EventXContext context, UserManager<ApplicationUser> userManager, FornecedorPerformanceService performanceService)
        {
            _context = context;
            _userManager = userManager;
            _performanceService = performanceService;
        }

        // GET: RankingFornecedor
        public async Task<IActionResult> Index(string? categoria, string? uf, string? ordem)
        {
            await _performanceService.RecalcularTodosAsync();

            var query = _context.FornecedorRankings
                .Include(r => r.Fornecedor)
                    .ThenInclude(f => f!.Pessoa)
                .Include(r => r.Fornecedor)
                    .ThenInclude(f => f!.Avaliacoes)
                .AsQueryable();

            if (!string.IsNullOrEmpty(categoria))
                query = query.Where(r => r.Fornecedor != null && r.Fornecedor.TipoServico.Contains(categoria));

            if (!string.IsNullOrEmpty(uf))
                query = query.Where(r => r.Fornecedor != null && r.Fornecedor.UF == uf);

            query = ordem switch
            {
                "avaliacao" => query.OrderByDescending(r => r.MediaAvaliacoes),
                "pedidos" => query.OrderByDescending(r => r.PedidosConcluidos),
                "eventos" => query.OrderByDescending(r => r.EventosAtendidos),
                "resposta" => query.OrderBy(r => r.TempoMedioRespostaHoras),
                _ => query.OrderByDescending(r => r.PontuacaoGeral)
            };

            var rankings = await query.ToListAsync();

            // Dados para filtros
            ViewBag.Categorias = await _context.Fornecedores
                .Select(f => f.TipoServico)
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync();

            ViewBag.UFs = await _context.Fornecedores
                .Select(f => f.UF)
                .Distinct()
                .OrderBy(u => u)
                .ToListAsync();

            ViewBag.FiltroCategoria = categoria;
            ViewBag.FiltroUF = uf;
            ViewBag.FiltroOrdem = ordem;

            // Estatísticas gerais
            ViewBag.TotalFornecedores = await _context.Fornecedores.CountAsync();
            ViewBag.TopFornecedores = rankings.Count(r => r.IsTopFornecedor);
            ViewBag.MediaGeralAvaliacoes = rankings.Any() ? rankings.Average(r => r.MediaAvaliacoes) : 0;

            return View(rankings);
        }

        // GET: RankingFornecedor/Perfil/5
        public async Task<IActionResult> Perfil(int id)
        {
            await _performanceService.RecalcularAsync(id);

            var ranking = await _context.FornecedorRankings
                .Include(r => r.Fornecedor)
                    .ThenInclude(f => f!.Pessoa)
                .Include(r => r.Fornecedor)
                    .ThenInclude(f => f!.Produtos)
                .Include(r => r.Fornecedor)
                    .ThenInclude(f => f!.Avaliacoes)
                .FirstOrDefaultAsync(r => r.FornecedorId == id);

            if (ranking?.Fornecedor == null)
            {
                var fornecedor = await _context.Fornecedores
                    .Include(f => f.Pessoa)
                    .FirstOrDefaultAsync(f => f.Id == id);
                if (fornecedor == null) return NotFound();

                await _performanceService.RecalcularAsync(id);
                ranking = await _context.FornecedorRankings
                    .Include(r => r.Fornecedor).ThenInclude(f => f!.Pessoa)
                    .Include(r => r.Fornecedor).ThenInclude(f => f!.Produtos)
                    .Include(r => r.Fornecedor).ThenInclude(f => f!.Avaliacoes)
                    .FirstOrDefaultAsync(r => r.FornecedorId == id);

                if (ranking == null) return NotFound();
            }

            // Avaliações recentes
            ViewBag.AvaliacoesRecentes = await _context.AvaliacoesFornecedores
                .Include(a => a.Organizador).ThenInclude(o => o!.Pessoa)
                .Include(a => a.Evento)
                .Where(a => a.FornecedorId == id)
                .OrderByDescending(a => a.DataAvaliacao)
                .Take(10)
                .ToListAsync();

            // Distribuição de notas
            var todasAvaliacoes = await _context.AvaliacoesFornecedores
                .Where(a => a.FornecedorId == id)
                .ToListAsync();

            ViewBag.Notas5 = todasAvaliacoes.Count(a => a.Nota == 5);
            ViewBag.Notas4 = todasAvaliacoes.Count(a => a.Nota == 4);
            ViewBag.Notas3 = todasAvaliacoes.Count(a => a.Nota == 3);
            ViewBag.Notas2 = todasAvaliacoes.Count(a => a.Nota == 2);
            ViewBag.Notas1 = todasAvaliacoes.Count(a => a.Nota == 1);
            ViewBag.TotalAvaliacoes = todasAvaliacoes.Count;

            // Pedidos recentes
            var produtoIds = ranking.Fornecedor!.Produtos.Select(p => p.Id).ToList();
            ViewBag.PedidosRecentes = await _context.Pedidos
                .Include(p => p.Evento)
                .Include(p => p.Produto)
                .Where(p => produtoIds.Contains(p.ProdutoId))
                .OrderByDescending(p => p.DataPedido)
                .Take(5)
                .ToListAsync();

            // Total de rankings para exibir posição
            ViewBag.TotalRanking = await _context.FornecedorRankings.CountAsync();

            return View(ranking);
        }

        // API JSON para componentes JS
        [HttpGet]
        public async Task<IActionResult> GetRankingData(int fornecedorId)
        {
            var ranking = await _context.FornecedorRankings
                .FirstOrDefaultAsync(r => r.FornecedorId == fornecedorId);

            if (ranking == null)
                return Json(new { success = false });

            return Json(new
            {
                success = true,
                data = new
                {
                    ranking.MediaAvaliacoes,
                    ranking.QuantidadeAvaliacoes,
                    ranking.PedidosConcluidos,
                    ranking.TempoMedioRespostaHoras,
                    ranking.TaxaAceitacao,
                    ranking.TaxaCancelamento,
                    ranking.EventosAtendidos,
                    ranking.PontuacaoGeral,
                    ranking.PosicaoRanking,
                    ranking.IsTopFornecedor
                }
            });
        }
    }
}
