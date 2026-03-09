using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoEventX.Data;
using ProjetoEventX.Models;
using ProjetoEventX.Services;

namespace ProjetoEventX.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly EventXContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FornecedorPerformanceService _performanceService;

        public FornecedorController(EventXContext context, UserManager<ApplicationUser> userManager, FornecedorPerformanceService performanceService)
        {
            _context = context;
            _userManager = userManager;
            _performanceService = performanceService;
        }

        // GET: Fornecedor
        public async Task<IActionResult> Index()
        {
            // Se o usuário logado é Organizador, redireciona para o catálogo de produtos
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null && user.TipoUsuario == "Organizador")
                {
                    return RedirectToAction(nameof(Catalogo));
                }
            }

            var eventXContext = _context.Fornecedores.Include(f => f.Pessoa);
            return View(await eventXContext.ToListAsync());
        }

        // GET: Fornecedor/Catalogo - Visualização de produtos para Organizadores
        public async Task<IActionResult> Catalogo()
        {
            var produtos = await _context.Produtos
                .Include(p => p.Fornecedor)
                    .ThenInclude(f => f.Pessoa)
                .OrderBy(p => p.Nome)
                .ToListAsync();

            var rankings = await _context.FornecedorRankings.ToListAsync();
            ViewBag.Rankings = rankings.ToDictionary(r => r.FornecedorId);

            return View(produtos);
        }

        // GET: Fornecedor/DetalhesProduto/guid - Detalhes do produto com info do fornecedor
        public async Task<IActionResult> DetalhesProduto(Guid? id)
        {
            if (id == null)
                return NotFound();

            var produto = await _context.Produtos
                .Include(p => p.Fornecedor)
                    .ThenInclude(f => f.Pessoa)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return NotFound();

            // Se o usuário logado é Organizador, carregar seus eventos para o modal de pedido
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null && user.TipoUsuario == "Organizador")
                {
                    var eventosOrganizador = await _context.Eventos
                        .Where(e => e.OrganizadorId == user.Id)
                        .OrderBy(e => e.NomeEvento)
                        .ToListAsync();

                    ViewBag.EventosOrganizador = eventosOrganizador;
                    ViewBag.IsOrganizador = true;
                }
            }

            return View(produto);
        }

        // GET: Fornecedor/Dashboard - Painel do Fornecedor
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Fornecedor")
                return RedirectToAction("LoginFornecedor", "Auth");

            var fornecedor = await _context.Fornecedores
                .Include(f => f.Pessoa)
                .Include(f => f.Produtos)
                .Include(f => f.Feedbacks)
                .Include(f => f.Avaliacoes)
                .FirstOrDefaultAsync(f => f.Email == user.Email);

            if (fornecedor == null)
                return NotFound();

            // Buscar pedidos pelos produtos do fornecedor (Pedido não tem FK direta para Fornecedor)
            var produtosIds = fornecedor.Produtos.Select(p => p.Id).ToList();
            var pedidosFornecedor = await _context.Pedidos
                .Include(p => p.Evento)
                .Include(p => p.Produto)
                .Where(p => produtosIds.Contains(p.ProdutoId))
                .OrderByDescending(p => p.DataPedido)
                .ToListAsync();

            // Estatísticas para o dashboard
            ViewBag.TotalProdutos = fornecedor.Produtos.Count;
            ViewBag.TotalPedidos = pedidosFornecedor.Count;
            ViewBag.PedidosPendentes = pedidosFornecedor.Count(p => p.StatusPedido == "Pendente");
            ViewBag.ReceitaTotal = pedidosFornecedor.Where(p => p.StatusPedido == "Pago" || p.StatusPedido == "Entregue").Sum(p => p.PrecoTotal);
            ViewBag.TotalFeedbacks = fornecedor.Feedbacks.Count;
            ViewBag.AvaliacaoMedia = fornecedor.AvaliacaoMedia;
            ViewBag.TotalAvaliacoes = fornecedor.Avaliacoes.Count;
            ViewBag.PedidosRecentes = pedidosFornecedor.Take(5).ToList();

            // Buscar avaliações recentes com dados do organizador
            ViewBag.AvaliacoesRecentes = await _context.AvaliacoesFornecedores
                .Include(a => a.Organizador)
                    .ThenInclude(o => o!.Pessoa)
                .Include(a => a.Evento)
                .Where(a => a.FornecedorId == fornecedor.Id)
                .OrderByDescending(a => a.DataAvaliacao)
                .Take(5)
                .ToListAsync();

            return View(fornecedor);
        }

        // GET: Fornecedor/Performance - Dashboard de Performance do Fornecedor
        public async Task<IActionResult> Performance()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Fornecedor")
                return RedirectToAction("LoginFornecedor", "Auth");

            var fornecedor = await _context.Fornecedores
                .Include(f => f.Pessoa)
                .Include(f => f.Produtos)
                .Include(f => f.Avaliacoes)
                .FirstOrDefaultAsync(f => f.Email == user.Email);

            if (fornecedor == null)
                return NotFound();

            // Recalcular métricas
            var ranking = await _performanceService.RecalcularAsync(fornecedor.Id);

            // Recarregar com includes
            ranking = await _context.FornecedorRankings
                .Include(r => r.Fornecedor)
                    .ThenInclude(f => f!.Pessoa)
                .FirstOrDefaultAsync(r => r.FornecedorId == fornecedor.Id);

            if (ranking == null)
                return NotFound();

            // Total de fornecedores para contexto de posição
            ViewBag.TotalFornecedores = await _context.FornecedorRankings.CountAsync();

            // Avaliações recentes
            ViewBag.AvaliacoesRecentes = await _context.AvaliacoesFornecedores
                .Include(a => a.Organizador).ThenInclude(o => o!.Pessoa)
                .Include(a => a.Evento)
                .Where(a => a.FornecedorId == fornecedor.Id)
                .OrderByDescending(a => a.DataAvaliacao)
                .Take(5)
                .ToListAsync();

            // Distribuição de notas
            var todasAvaliacoes = await _context.AvaliacoesFornecedores
                .Where(a => a.FornecedorId == fornecedor.Id)
                .ToListAsync();

            ViewBag.Notas5 = todasAvaliacoes.Count(a => a.Nota == 5);
            ViewBag.Notas4 = todasAvaliacoes.Count(a => a.Nota == 4);
            ViewBag.Notas3 = todasAvaliacoes.Count(a => a.Nota == 3);
            ViewBag.Notas2 = todasAvaliacoes.Count(a => a.Nota == 2);
            ViewBag.Notas1 = todasAvaliacoes.Count(a => a.Nota == 1);

            // Pedidos recentes
            var produtoIds = fornecedor.Produtos.Select(p => p.Id).ToList();
            ViewBag.PedidosRecentes = await _context.Pedidos
                .Include(p => p.Evento)
                .Include(p => p.Produto)
                .Where(p => produtoIds.Contains(p.ProdutoId))
                .OrderByDescending(p => p.DataPedido)
                .Take(10)
                .ToListAsync();

            // Receita total
            ViewBag.ReceitaTotal = await _context.Pedidos
                .Where(p => produtoIds.Contains(p.ProdutoId)
                    && (p.StatusPedido == "Pago" || p.StatusPedido == "Entregue"))
                .SumAsync(p => (decimal?)p.PrecoTotal) ?? 0;

            // Quotes pendentes
            ViewBag.QuotesPendentes = await _context.Quotes
                .CountAsync(q => q.SupplierId == fornecedor.Id
                    && (q.Status == "Pendente" || q.Status == "EmNegociacao"));

            return View(ranking);
        }

        // POST: Fornecedor/AddProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(string ProductName, string ProductDescription, decimal ProductPrice, string ProductType)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Fornecedor")
                return RedirectToAction("LoginFornecedor", "Auth");

            var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Email == user.Email);
            if (fornecedor == null)
                return NotFound();

            var produto = new Produto
            {
                Nome = ProductName,
                Descricao = ProductDescription,
                Preco = ProductPrice,
                Tipo = ProductType ?? "Produto",
                FornecedorId = fornecedor.Id,
                Fornecedor = fornecedor
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Dashboard));
        }

        // POST: Fornecedor/EditarProduto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarProduto(Guid produtoId, string Nome, string Descricao, decimal Preco, string Tipo)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Fornecedor")
                return RedirectToAction("LoginFornecedor", "Auth");

            var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Email == user.Email);
            if (fornecedor == null)
                return NotFound();

            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == produtoId && p.FornecedorId == fornecedor.Id);
            if (produto == null)
                return NotFound();

            produto.Nome = Nome;
            produto.Descricao = Descricao;
            produto.Preco = Preco;
            produto.Tipo = Tipo ?? produto.Tipo;

            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Produto atualizado com sucesso!";
            return RedirectToAction(nameof(Dashboard));
        }

        // POST: Fornecedor/ExcluirProduto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirProduto(Guid produtoId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Fornecedor")
                return RedirectToAction("LoginFornecedor", "Auth");

            var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Email == user.Email);
            if (fornecedor == null)
                return NotFound();

            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == produtoId && p.FornecedorId == fornecedor.Id);
            if (produto == null)
                return NotFound();

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Produto excluído com sucesso!";
            return RedirectToAction(nameof(Dashboard));
        }

        // GET: Fornecedor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores
                .Include(f => f.Pessoa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // GET: Fornecedor/Create
        public IActionResult Create()
        {
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "Id", "Email");
            return View();
        }

        // POST: Fornecedor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PessoaId,Cnpj,TipoServico,AvaliacaoMedia,DataCadastro,CreatedAt,UpdatedAt,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fornecedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "Id", "Email", fornecedor.PessoaId);
            return View(fornecedor);
        }

        // GET: Fornecedor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "Id", "Email", fornecedor.PessoaId);
            return View(fornecedor);
        }

        // POST: Fornecedor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PessoaId,Cnpj,TipoServico,AvaliacaoMedia,DataCadastro,CreatedAt,UpdatedAt,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Fornecedor fornecedor)
        {
            if (id != fornecedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fornecedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedorExists(fornecedor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PessoaId"] = new SelectList(_context.Pessoas, "Id", "Email", fornecedor.PessoaId);
            return View(fornecedor);
        }

        // GET: Fornecedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores
                .Include(f => f.Pessoa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // POST: Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor != null)
            {
                _context.Fornecedores.Remove(fornecedor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Fornecedor/EditarPerfil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPerfil(string NomeLoja, string Cnpj, string Telefone, string Cidade, string UF, string TipoServico, string Endereco)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Fornecedor")
                return RedirectToAction("LoginFornecedor", "Auth");

            var fornecedor = await _context.Fornecedores
                .Include(f => f.Pessoa)
                .FirstOrDefaultAsync(f => f.Email == user.Email);

            if (fornecedor == null)
                return NotFound();

            // Atualizar dados do Fornecedor
            if (!string.IsNullOrWhiteSpace(Cnpj))
                fornecedor.Cnpj = Cnpj;
            if (!string.IsNullOrWhiteSpace(Cidade))
                fornecedor.Cidade = Cidade;
            if (!string.IsNullOrWhiteSpace(UF))
                fornecedor.UF = UF;
            if (!string.IsNullOrWhiteSpace(TipoServico))
                fornecedor.TipoServico = TipoServico;
            if (!string.IsNullOrWhiteSpace(Telefone))
                fornecedor.PhoneNumber = Telefone;
            fornecedor.UpdatedAt = DateTime.Now;

            // Atualizar dados da Pessoa vinculada
            if (fornecedor.Pessoa != null)
            {
                if (!string.IsNullOrWhiteSpace(NomeLoja))
                    fornecedor.Pessoa.Nome = NomeLoja;
                if (!string.IsNullOrWhiteSpace(Telefone))
                    fornecedor.Pessoa.Telefone = Telefone;
                if (!string.IsNullOrWhiteSpace(Cidade))
                    fornecedor.Pessoa.Cidade = Cidade;
                if (!string.IsNullOrWhiteSpace(UF))
                    fornecedor.Pessoa.UF = UF;
                if (!string.IsNullOrWhiteSpace(Endereco))
                    fornecedor.Pessoa.Endereco = Endereco;
                fornecedor.Pessoa.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Informações da loja atualizadas com sucesso!";
            return RedirectToAction(nameof(Dashboard));
        }

        private bool FornecedorExists(int id)
        {
            return _context.Fornecedores.Any(e => e.Id == id);
        }
    }
}
