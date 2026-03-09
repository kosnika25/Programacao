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
    public class OrcamentoController : Controller
    {
        private readonly EventXContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly NotificationService _notificationService;
        private readonly EventLogService _eventLogService;

        public OrcamentoController(EventXContext context, UserManager<ApplicationUser> userManager, NotificationService notificationService, EventLogService eventLogService)
        {
            _context = context;
            _userManager = userManager;
            _notificationService = notificationService;
            _eventLogService = eventLogService;
        }

        // GET: Orcamento
        public async Task<IActionResult> Index(string? status, int? eventoId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("LoginOrganizador", "Auth");

            IQueryable<Quote> query = _context.Quotes
                .Include(q => q.Event)
                .Include(q => q.Supplier)
                    .ThenInclude(s => s!.Pessoa)
                .Include(q => q.PedidoGerado);

            if (user.TipoUsuario == "Organizador")
            {
                query = query.Where(q => q.OrganizadorId == user.Id);
            }
            else if (user.TipoUsuario == "Fornecedor")
            {
                var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Email == user.Email);
                if (fornecedor == null)
                    return RedirectToAction("LoginFornecedor", "Auth");
                query = query.Where(q => q.SupplierId == fornecedor.Id);
            }
            else
            {
                return Forbid();
            }

            if (!string.IsNullOrEmpty(status))
                query = query.Where(q => q.Status == status);

            if (eventoId.HasValue)
                query = query.Where(q => q.EventId == eventoId.Value);

            var orcamentos = await query.OrderByDescending(q => q.CreatedAt).ToListAsync();

            // Eventos do organizador para filtro
            if (user.TipoUsuario == "Organizador")
            {
                ViewBag.Eventos = await _context.Eventos
                    .Where(e => e.OrganizadorId == user.Id)
                    .OrderBy(e => e.NomeEvento)
                    .ToListAsync();
            }

            ViewBag.FiltroStatus = status;
            ViewBag.FiltroEventoId = eventoId;
            ViewBag.TipoUsuario = user.TipoUsuario;

            // Estatísticas
            List<Quote> todos;
            if (user.TipoUsuario == "Organizador")
            {
                todos = await _context.Quotes.Where(q => q.OrganizadorId == user.Id).ToListAsync();
            }
            else
            {
                var forn = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Email == user.Email);
                todos = await _context.Quotes.Where(q => q.SupplierId == forn!.Id).ToListAsync();
            }

            ViewBag.TotalOrcamentos = todos.Count;
            ViewBag.Pendentes = todos.Count(q => q.Status == "Pendente");
            ViewBag.Respondidos = todos.Count(q => q.Status == "Respondido");
            ViewBag.EmNegociacao = todos.Count(q => q.Status == "EmNegociacao");
            ViewBag.Aceitos = todos.Count(q => q.Status == "Aceito");
            ViewBag.Recusados = todos.Count(q => q.Status == "Recusado");
            ViewBag.ValorTotal = todos.Where(q => q.Status == "Aceito").Sum(q => q.ResponseValue ?? q.EstimatedValue);

            return View(orcamentos);
        }

        // GET: Orcamento/Detalhes/5
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("LoginOrganizador", "Auth");

            var quote = await _context.Quotes
                .Include(q => q.Event)
                .Include(q => q.Supplier)
                    .ThenInclude(s => s!.Pessoa)
                .Include(q => q.Organizador)
                    .ThenInclude(o => o!.Pessoa)
                .Include(q => q.PedidoGerado)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quote == null)
                return NotFound();

            // Verificar permissão
            if (user.TipoUsuario == "Organizador" && quote.OrganizadorId != user.Id)
                return Forbid();

            if (user.TipoUsuario == "Fornecedor")
            {
                var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Email == user.Email);
                if (fornecedor == null || quote.SupplierId != fornecedor.Id)
                    return Forbid();
            }

            ViewBag.TipoUsuario = user.TipoUsuario;

            // Chat: contagem de mensagens não lidas
            ViewBag.UnreadMessages = await _context.QuoteMessages
                .CountAsync(m => m.QuoteId == id && !m.IsRead && m.SenderUserId != user.Id);
            ViewBag.CurrentUserId = user.Id;

            // Histórico de negociação
            ViewBag.Historico = await _context.NegociacaoHistoricos
                .Where(h => h.QuoteId == id)
                .OrderBy(h => h.Rodada)
                .ThenBy(h => h.DataAcao)
                .ToListAsync();

            return View(quote);
        }

        // GET: Orcamento/Criar
        [HttpGet]
        public async Task<IActionResult> Criar(int? fornecedorId, int? eventoId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Organizador")
                return RedirectToAction("LoginOrganizador", "Auth");

            ViewBag.Eventos = await _context.Eventos
                .Where(e => e.OrganizadorId == user.Id)
                .OrderBy(e => e.NomeEvento)
                .ToListAsync();

            ViewBag.Fornecedores = await _context.Fornecedores
                .Include(f => f.Pessoa)
                .OrderBy(f => f.Pessoa.Nome)
                .ToListAsync();

            ViewBag.FornecedorIdSelecionado = fornecedorId;
            ViewBag.EventoIdSelecionado = eventoId;

            return View();
        }

        // POST: Orcamento/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(int eventoId, int supplierId, string serviceName, string description, decimal estimatedValue, DateTime? prazoValidade)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Organizador")
                return RedirectToAction("LoginOrganizador", "Auth");

            // Verificar se o evento pertence ao organizador
            var evento = await _context.Eventos.FirstOrDefaultAsync(e => e.Id == eventoId && e.OrganizadorId == user.Id);
            if (evento == null)
            {
                TempData["ErrorMessage"] = "Evento não encontrado ou sem permissão.";
                return RedirectToAction(nameof(Criar));
            }

            // Verificar se o fornecedor existe
            var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Id == supplierId);
            if (fornecedor == null)
            {
                TempData["ErrorMessage"] = "Fornecedor não encontrado.";
                return RedirectToAction(nameof(Criar));
            }

            if (string.IsNullOrWhiteSpace(serviceName) || string.IsNullOrWhiteSpace(description))
            {
                TempData["ErrorMessage"] = "Serviço e descrição são obrigatórios.";
                return RedirectToAction(nameof(Criar), new { fornecedorId = supplierId, eventoId });
            }

            var quote = new Quote
            {
                EventId = eventoId,
                SupplierId = supplierId,
                OrganizadorId = user.Id,
                ServiceName = serviceName,
                Description = description,
                EstimatedValue = estimatedValue,
                Status = "Pendente",
                CreatedAt = DateTime.UtcNow,
                RodadaAtual = 1,
                PrazoValidade = prazoValidade
            };

            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();

            // Registrar no histórico
            _context.NegociacaoHistoricos.Add(new NegociacaoHistorico
            {
                QuoteId = quote.Id,
                Rodada = 1,
                TipoAcao = "Solicitacao",
                Valor = estimatedValue,
                Mensagem = description,
                UsuarioId = user.Id,
                TipoUsuario = "Organizador",
                DataAcao = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Solicitação de orçamento enviada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // POST: Orcamento/Responder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Responder(int id, string responseMessage, decimal? responseValue)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Fornecedor")
                return RedirectToAction("LoginFornecedor", "Auth");

            var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Email == user.Email);
            if (fornecedor == null)
                return NotFound();

            var quote = await _context.Quotes.FirstOrDefaultAsync(q => q.Id == id && q.SupplierId == fornecedor.Id);
            if (quote == null)
                return NotFound();

            if (quote.Status != "Pendente")
            {
                TempData["ErrorMessage"] = "Este orçamento já foi respondido ou está em outra fase.";
                return RedirectToAction(nameof(Detalhes), new { id });
            }

            quote.Status = "Respondido";
            quote.ResponseMessage = responseMessage;
            quote.ResponseValue = responseValue;
            quote.ResponseDate = DateTime.UtcNow;

            // Registrar no histórico
            _context.NegociacaoHistoricos.Add(new NegociacaoHistorico
            {
                QuoteId = quote.Id,
                Rodada = quote.RodadaAtual,
                TipoAcao = "Resposta",
                Valor = responseValue,
                Mensagem = responseMessage,
                UsuarioId = fornecedor.Id,
                TipoUsuario = "Fornecedor",
                DataAcao = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            // Notificar organizador sobre a resposta
            await _notificationService.CreateAsync(
                quote.OrganizadorId,
                "Resposta de orçamento recebida",
                $"O fornecedor respondeu ao orçamento de '{quote.ServiceName}'.",
                "RespostaOrcamento",
                $"/Orcamento/Detalhes/{id}");

            await _eventLogService.LogAsync(quote.EventId, fornecedor.Id, "RespostaFornecedor",
                $"Fornecedor respondeu ao orçamento de '{quote.ServiceName}'" + (responseValue.HasValue ? $" com valor R$ {responseValue:N2}" : ""));

            TempData["SuccessMessage"] = "Orçamento respondido com sucesso!";
            return RedirectToAction(nameof(Detalhes), new { id });
        }

        // POST: Orcamento/Aceitar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aceitar(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Organizador")
                return RedirectToAction("LoginOrganizador", "Auth");

            var quote = await _context.Quotes
                .Include(q => q.Supplier)
                .Include(q => q.Event)
                .FirstOrDefaultAsync(q => q.Id == id && q.OrganizadorId == user.Id);

            if (quote == null)
                return NotFound();

            if (quote.Status != "Respondido")
            {
                TempData["ErrorMessage"] = "Apenas orçamentos respondidos podem ser aceitos.";
                return RedirectToAction(nameof(Detalhes), new { id });
            }

            quote.Status = "Aceito";

            // Registrar no histórico
            _context.NegociacaoHistoricos.Add(new NegociacaoHistorico
            {
                QuoteId = quote.Id,
                Rodada = quote.RodadaAtual,
                TipoAcao = "Aceite",
                Valor = quote.ResponseValue ?? quote.EstimatedValue,
                Mensagem = "Proposta aceita pelo organizador",
                UsuarioId = user.Id,
                TipoUsuario = "Organizador",
                DataAcao = DateTime.UtcNow
            });

            // Criar automaticamente um Pedido
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.FornecedorId == quote.SupplierId);

            if (produto == null)
            {
                // Criar um produto de serviço para o fornecedor
                produto = new Produto
                {
                    Nome = quote.ServiceName,
                    Descricao = quote.Description,
                    Preco = quote.ResponseValue ?? quote.EstimatedValue,
                    Tipo = "Serviço",
                    FornecedorId = quote.SupplierId,
                    Fornecedor = quote.Supplier!
                };
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();
            }

            var pedido = new Pedido
            {
                EventoId = quote.EventId,
                ProdutoId = produto.Id,
                Quantidade = 1,
                PrecoTotal = quote.ResponseValue ?? quote.EstimatedValue,
                StatusPedido = "Pendente",
                DataPedido = DateTime.UtcNow,
                DespesaGerada = false
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            quote.PedidoGeradoId = pedido.Id;
            await _context.SaveChangesAsync();

            // Notificar fornecedor sobre aceitação
            var supplierUser = await _userManager.FindByEmailAsync(quote.Supplier?.Email ?? "");
            if (supplierUser != null)
            {
                await _notificationService.CreateAsync(
                    supplierUser.Id,
                    "Proposta aceita!",
                    $"Sua proposta para '{quote.ServiceName}' foi aceita pelo organizador.",
                    "PropostaAceita",
                    $"/Orcamento/Detalhes/{id}");
            }

            TempData["SuccessMessage"] = "Orçamento aceito! Um pedido foi criado automaticamente.";

            await _eventLogService.LogAsync(quote.EventId, user.Id, "PropostaAceita",
                $"Proposta de '{quote.ServiceName}' aceita. Pedido criado automaticamente.");

            return RedirectToAction(nameof(Detalhes), new { id });
        }

        // POST: Orcamento/Recusar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Recusar(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Organizador")
                return RedirectToAction("LoginOrganizador", "Auth");

            var quote = await _context.Quotes
                .Include(q => q.Supplier)
                .FirstOrDefaultAsync(q => q.Id == id && q.OrganizadorId == user.Id);
            if (quote == null)
                return NotFound();

            if (quote.Status != "Respondido" && quote.Status != "Pendente")
            {
                TempData["ErrorMessage"] = "Este orçamento não pode ser recusado.";
                return RedirectToAction(nameof(Detalhes), new { id });
            }

            quote.Status = "Recusado";

            // Registrar no histórico
            _context.NegociacaoHistoricos.Add(new NegociacaoHistorico
            {
                QuoteId = quote.Id,
                Rodada = quote.RodadaAtual,
                TipoAcao = "Recusa",
                Mensagem = "Proposta recusada pelo organizador",
                UsuarioId = user.Id,
                TipoUsuario = "Organizador",
                DataAcao = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            // Notificar fornecedor sobre recusa
            var supplierUser = await _userManager.FindByEmailAsync(quote.Supplier?.Email ?? "");
            if (supplierUser != null)
            {
                await _notificationService.CreateAsync(
                    supplierUser.Id,
                    "Proposta recusada",
                    $"Sua proposta para '{quote.ServiceName}' foi recusada pelo organizador.",
                    "PropostaRecusada",
                    $"/Orcamento/Detalhes/{id}");
            }

            TempData["SuccessMessage"] = "Orçamento recusado.";

            await _eventLogService.LogAsync(quote.EventId, user.Id, "PropostaRecusada",
                $"Proposta de '{quote.ServiceName}' recusada pelo organizador.");

            return RedirectToAction(nameof(Index));
        }

        // POST: Orcamento/Cancelar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Forbid();

            var quote = await _context.Quotes.FirstOrDefaultAsync(q => q.Id == id && q.OrganizadorId == user.Id);
            if (quote == null)
                return NotFound();

            if (quote.Status == "Aceito")
            {
                TempData["ErrorMessage"] = "Orçamentos aceitos não podem ser cancelados.";
                return RedirectToAction(nameof(Detalhes), new { id });
            }

            quote.Status = "Cancelado";

            // Registrar no histórico
            _context.NegociacaoHistoricos.Add(new NegociacaoHistorico
            {
                QuoteId = quote.Id,
                Rodada = quote.RodadaAtual,
                TipoAcao = "Cancelamento",
                Mensagem = "Orçamento cancelado pelo organizador",
                UsuarioId = user.Id,
                TipoUsuario = "Organizador",
                DataAcao = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Orçamento cancelado.";
            return RedirectToAction(nameof(Index));
        }

        // ==================== CONTRA-PROPOSTA ====================

        // POST: Orcamento/ContraProposta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContraProposta(int id, decimal contraPropostaValor, string contraPropostaMensagem)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Organizador")
                return RedirectToAction("LoginOrganizador", "Auth");

            var quote = await _context.Quotes
                .Include(q => q.Supplier)
                .FirstOrDefaultAsync(q => q.Id == id && q.OrganizadorId == user.Id);
            if (quote == null)
                return NotFound();

            if (quote.Status != "Respondido")
            {
                TempData["ErrorMessage"] = "Só é possível fazer contra-proposta para orçamentos respondidos.";
                return RedirectToAction(nameof(Detalhes), new { id });
            }

            if (string.IsNullOrWhiteSpace(contraPropostaMensagem))
            {
                TempData["ErrorMessage"] = "A mensagem da contra-proposta é obrigatória.";
                return RedirectToAction(nameof(Detalhes), new { id });
            }

            quote.ContraPropostaValor = contraPropostaValor;
            quote.ContraPropostaMensagem = contraPropostaMensagem;
            quote.DataContraProposta = DateTime.UtcNow;
            quote.Status = "EmNegociacao";
            quote.RodadaAtual++;

            // Registrar no histórico
            _context.NegociacaoHistoricos.Add(new NegociacaoHistorico
            {
                QuoteId = quote.Id,
                Rodada = quote.RodadaAtual,
                TipoAcao = "ContraProposta",
                Valor = contraPropostaValor,
                Mensagem = contraPropostaMensagem,
                UsuarioId = user.Id,
                TipoUsuario = "Organizador",
                DataAcao = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            // Notificar fornecedor sobre contra-proposta
            var supplierUser = await _userManager.FindByEmailAsync(quote.Supplier?.Email ?? "");
            if (supplierUser != null)
            {
                await _notificationService.CreateAsync(
                    supplierUser.Id,
                    "Nova contra-proposta recebida",
                    $"O organizador enviou uma contra-proposta de R$ {contraPropostaValor:N2} para '{quote.ServiceName}'.",
                    "RespostaOrcamento",
                    $"/Orcamento/Detalhes/{id}");
            }

            TempData["SuccessMessage"] = "Contra-proposta enviada com sucesso! Aguarde a resposta do fornecedor.";

            await _eventLogService.LogAsync(quote.EventId, user.Id, "SolicitacaoOrcamento",
                $"Contra-proposta de R$ {contraPropostaValor:N2} enviada para '{quote.ServiceName}'.");

            return RedirectToAction(nameof(Detalhes), new { id });
        }

        // POST: Orcamento/ResponderContraProposta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResponderContraProposta(int id, string acao, decimal? novoValor, string? novaMensagem)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Fornecedor")
                return RedirectToAction("LoginFornecedor", "Auth");

            var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Email == user.Email);
            if (fornecedor == null)
                return NotFound();

            var quote = await _context.Quotes.FirstOrDefaultAsync(q => q.Id == id && q.SupplierId == fornecedor.Id);
            if (quote == null)
                return NotFound();

            if (quote.Status != "EmNegociacao")
            {
                TempData["ErrorMessage"] = "Este orçamento não está em negociação.";
                return RedirectToAction(nameof(Detalhes), new { id });
            }

            if (acao == "aceitar")
            {
                // Aceitar a contra-proposta: atualizar o valor proposto para o valor da contra-proposta
                quote.ResponseValue = quote.ContraPropostaValor;
                quote.ResponseMessage = novaMensagem ?? "Contra-proposta aceita pelo fornecedor.";
                quote.ResponseDate = DateTime.UtcNow;
                quote.Status = "Respondido";

                _context.NegociacaoHistoricos.Add(new NegociacaoHistorico
                {
                    QuoteId = quote.Id,
                    Rodada = quote.RodadaAtual,
                    TipoAcao = "Resposta",
                    Valor = quote.ContraPropostaValor,
                    Mensagem = quote.ResponseMessage,
                    UsuarioId = fornecedor.Id,
                    TipoUsuario = "Fornecedor",
                    DataAcao = DateTime.UtcNow
                });

                TempData["SuccessMessage"] = "Contra-proposta aceita! O organizador agora pode finalizar a negociação.";
            }
            else if (acao == "nova-oferta")
            {
                if (!novoValor.HasValue || novoValor.Value <= 0)
                {
                    TempData["ErrorMessage"] = "Informe o valor da nova oferta.";
                    return RedirectToAction(nameof(Detalhes), new { id });
                }

                // Nova oferta do fornecedor
                quote.ResponseValue = novoValor.Value;
                quote.ResponseMessage = novaMensagem ?? "Nova oferta do fornecedor.";
                quote.ResponseDate = DateTime.UtcNow;
                quote.Status = "Respondido";

                _context.NegociacaoHistoricos.Add(new NegociacaoHistorico
                {
                    QuoteId = quote.Id,
                    Rodada = quote.RodadaAtual,
                    TipoAcao = "NovaOferta",
                    Valor = novoValor.Value,
                    Mensagem = quote.ResponseMessage,
                    UsuarioId = fornecedor.Id,
                    TipoUsuario = "Fornecedor",
                    DataAcao = DateTime.UtcNow
                });

                TempData["SuccessMessage"] = "Nova oferta enviada com sucesso!";
            }
            else
            {
                TempData["ErrorMessage"] = "Ação inválida.";
                return RedirectToAction(nameof(Detalhes), new { id });
            }

            await _context.SaveChangesAsync();

            // Notificar organizador sobre a resposta à contra-proposta
            await _notificationService.CreateAsync(
                quote.OrganizadorId,
                "Resposta à contra-proposta",
                $"O fornecedor respondeu à sua contra-proposta para '{quote.ServiceName}'.",
                "RespostaOrcamento",
                $"/Orcamento/Detalhes/{id}");

            return RedirectToAction(nameof(Detalhes), new { id });
        }

        // ==================== ENVIO EM LOTE ====================

        // POST: Orcamento/CriarLote
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarLote(int eventoId, int[] supplierIds, string serviceName, string description, decimal estimatedValue, DateTime? prazoValidade)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Organizador")
                return RedirectToAction("LoginOrganizador", "Auth");

            var evento = await _context.Eventos.FirstOrDefaultAsync(e => e.Id == eventoId && e.OrganizadorId == user.Id);
            if (evento == null)
            {
                TempData["ErrorMessage"] = "Evento não encontrado ou sem permissão.";
                return RedirectToAction(nameof(Criar));
            }

            if (supplierIds == null || supplierIds.Length == 0)
            {
                TempData["ErrorMessage"] = "Selecione pelo menos um fornecedor.";
                return RedirectToAction(nameof(Criar));
            }

            if (string.IsNullOrWhiteSpace(serviceName) || string.IsNullOrWhiteSpace(description))
            {
                TempData["ErrorMessage"] = "Serviço e descrição são obrigatórios.";
                return RedirectToAction(nameof(Criar));
            }

            var fornecedores = await _context.Fornecedores
                .Where(f => supplierIds.Contains(f.Id))
                .ToListAsync();

            int criados = 0;
            foreach (var fornecedor in fornecedores)
            {
                var quote = new Quote
                {
                    EventId = eventoId,
                    SupplierId = fornecedor.Id,
                    OrganizadorId = user.Id,
                    ServiceName = serviceName,
                    Description = description,
                    EstimatedValue = estimatedValue,
                    Status = "Pendente",
                    CreatedAt = DateTime.UtcNow,
                    RodadaAtual = 1,
                    PrazoValidade = prazoValidade
                };
                _context.Quotes.Add(quote);
                await _context.SaveChangesAsync();

                _context.NegociacaoHistoricos.Add(new NegociacaoHistorico
                {
                    QuoteId = quote.Id,
                    Rodada = 1,
                    TipoAcao = "Solicitacao",
                    Valor = estimatedValue,
                    Mensagem = description,
                    UsuarioId = user.Id,
                    TipoUsuario = "Organizador",
                    DataAcao = DateTime.UtcNow
                });

                // Notificar fornecedor sobre novo orçamento
                var supplierUser = await _userManager.FindByEmailAsync(fornecedor.Email ?? "");
                if (supplierUser != null)
                {
                    await _notificationService.CreateAsync(
                        supplierUser.Id,
                        "Nova solicitação de orçamento",
                        $"Você recebeu uma solicitação de orçamento para '{serviceName}'.",
                        "NovoPedido",
                        $"/Orcamento/Detalhes/{quote.Id}");
                }

                criados++;
            }

            await _context.SaveChangesAsync();

            await _eventLogService.LogAsync(eventoId, user.Id, "SolicitacaoOrcamento",
                $"Solicitação de orçamento enviada para {criados} fornecedor(es) — serviço: '{serviceName}'.");

            TempData["SuccessMessage"] = $"Solicitação enviada para {criados} fornecedor(es) com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // ==================== HISTÓRICO DE NEGOCIAÇÃO ====================

        // GET: Orcamento/Historico/5
        public async Task<IActionResult> Historico(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Forbid();

            var quote = await _context.Quotes
                .Include(q => q.Event)
                .Include(q => q.Supplier)
                    .ThenInclude(s => s!.Pessoa)
                .Include(q => q.Organizador)
                    .ThenInclude(o => o!.Pessoa)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quote == null)
                return NotFound();

            // Verificar permissão
            if (user.TipoUsuario == "Organizador" && quote.OrganizadorId != user.Id)
                return Forbid();

            if (user.TipoUsuario == "Fornecedor")
            {
                var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Email == user.Email);
                if (fornecedor == null || quote.SupplierId != fornecedor.Id)
                    return Forbid();
            }

            var historico = await _context.NegociacaoHistoricos
                .Where(h => h.QuoteId == id)
                .OrderBy(h => h.Rodada)
                .ThenBy(h => h.DataAcao)
                .ToListAsync();

            ViewBag.Quote = quote;
            ViewBag.TipoUsuario = user.TipoUsuario;

            return View(historico);
        }

        // ==================== COMPARAÇÃO DE ORÇAMENTOS ====================

        // GET: Orcamento/Comparar?ids=1,2,3
        [HttpGet]
        public async Task<IActionResult> Comparar(string ids)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.TipoUsuario != "Organizador")
                return RedirectToAction("LoginOrganizador", "Auth");

            if (string.IsNullOrWhiteSpace(ids))
            {
                TempData["ErrorMessage"] = "Selecione pelo menos 2 orçamentos para comparar.";
                return RedirectToAction(nameof(Index));
            }

            var idList = ids.Split(',')
                .Select(s => int.TryParse(s.Trim(), out int v) ? v : 0)
                .Where(v => v > 0)
                .Distinct()
                .ToList();

            if (idList.Count < 2)
            {
                TempData["ErrorMessage"] = "Selecione pelo menos 2 orçamentos para comparar.";
                return RedirectToAction(nameof(Index));
            }

            var quotes = await _context.Quotes
                .Include(q => q.Event)
                .Include(q => q.Supplier)
                    .ThenInclude(s => s!.Pessoa)
                .Include(q => q.Supplier)
                    .ThenInclude(s => s!.Avaliacoes)
                .Where(q => idList.Contains(q.Id) && q.OrganizadorId == user.Id)
                .ToListAsync();

            if (quotes.Count < 2)
            {
                TempData["ErrorMessage"] = "Não foi possível encontrar os orçamentos selecionados.";
                return RedirectToAction(nameof(Index));
            }

            // Carregar dados de ranking dos fornecedores para comparação
            var supplierIds = quotes.Select(q => q.SupplierId).Distinct().ToList();
            var rankings = await _context.FornecedorRankings
                .Where(r => supplierIds.Contains(r.FornecedorId))
                .ToDictionaryAsync(r => r.FornecedorId);
            ViewBag.Rankings = rankings;

            return View(quotes);
        }

        // ==================== CHAT DE ORÇAMENTOS ====================

        // GET: Orcamento/GetMessages?quoteId=5
        [HttpGet]
        public async Task<IActionResult> GetMessages(int quoteId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var quote = await _context.Quotes.FindAsync(quoteId);
            if (quote == null)
                return NotFound();

            // Verificar permissão
            if (user.TipoUsuario == "Organizador" && quote.OrganizadorId != user.Id)
                return Forbid();

            if (user.TipoUsuario == "Fornecedor")
            {
                var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Email == user.Email);
                if (fornecedor == null || quote.SupplierId != fornecedor.Id)
                    return Forbid();
            }

            var messages = await _context.QuoteMessages
                .Where(m => m.QuoteId == quoteId)
                .OrderBy(m => m.SentAt)
                .Select(m => new
                {
                    m.Id,
                    m.SenderUserId,
                    m.SenderType,
                    m.Message,
                    SentAt = m.SentAt.ToString("dd/MM/yyyy HH:mm"),
                    m.IsRead
                })
                .ToListAsync();

            // Marcar mensagens do outro usuário como lidas
            var unread = await _context.QuoteMessages
                .Where(m => m.QuoteId == quoteId && !m.IsRead && m.SenderUserId != user.Id)
                .ToListAsync();

            foreach (var msg in unread)
                msg.IsRead = true;

            if (unread.Any())
                await _context.SaveChangesAsync();

            return Json(new { success = true, messages, currentUserId = user.Id });
        }

        // POST: Orcamento/SendMessage
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            if (string.IsNullOrWhiteSpace(request.Message))
                return Json(new { success = false, message = "Mensagem não pode estar vazia." });

            var quote = await _context.Quotes.FindAsync(request.QuoteId);
            if (quote == null)
                return NotFound();

            // Verificar permissão
            if (user.TipoUsuario == "Organizador" && quote.OrganizadorId != user.Id)
                return Forbid();

            if (user.TipoUsuario == "Fornecedor")
            {
                var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Email == user.Email);
                if (fornecedor == null || quote.SupplierId != fornecedor.Id)
                    return Forbid();
            }

            var msg = new QuoteMessage
            {
                QuoteId = request.QuoteId,
                SenderUserId = user.Id,
                SenderType = user.TipoUsuario ?? "Organizador",
                Message = request.Message.Trim(),
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            _context.QuoteMessages.Add(msg);
            await _context.SaveChangesAsync();

            await _eventLogService.LogAsync(quote.EventId, user.Id, "MensagemChat",
                $"Mensagem enviada no chat do orçamento '{quote.ServiceName}'.");

            // Notificar o outro participante sobre nova mensagem
            if (user.TipoUsuario == "Organizador")
            {
                var supplierEntity = await _context.Fornecedores.FindAsync(quote.SupplierId);
                var supplierUser = supplierEntity != null ? await _userManager.FindByEmailAsync(supplierEntity.Email ?? "") : null;
                if (supplierUser != null)
                {
                    await _notificationService.CreateAsync(
                        supplierUser.Id,
                        "Nova mensagem no chat",
                        $"Você recebeu uma nova mensagem sobre o orçamento '{quote.ServiceName}'.",
                        "NovaMensagem",
                        $"/Orcamento/Detalhes/{quote.Id}");
                }
            }
            else
            {
                await _notificationService.CreateAsync(
                    quote.OrganizadorId,
                    "Nova mensagem no chat",
                    $"Você recebeu uma nova mensagem sobre o orçamento '{quote.ServiceName}'.",
                    "NovaMensagem",
                    $"/Orcamento/Detalhes/{quote.Id}");
            }

            return Json(new
            {
                success = true,
                message = new
                {
                    msg.Id,
                    msg.SenderUserId,
                    msg.SenderType,
                    msg.Message,
                    SentAt = msg.SentAt.ToString("dd/MM/yyyy HH:mm"),
                    msg.IsRead
                }
            });
        }

        // GET: Orcamento/UnreadCount?quoteId=5
        [HttpGet]
        public async Task<IActionResult> UnreadCount(int quoteId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var count = await _context.QuoteMessages
                .CountAsync(m => m.QuoteId == quoteId && !m.IsRead && m.SenderUserId != user.Id);

            return Json(new { count });
        }
    }

    public class SendMessageRequest
    {
        public int QuoteId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
