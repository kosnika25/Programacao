using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjetoEventX.Data;
using ProjetoEventX.Models;
namespace ProjetoEventX.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly EventXContext _context;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatController(EventXContext context, IHubContext<ChatHub> hubContext, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Chat(int eventoId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Forbid();

            var evento = await _context.Eventos.FindAsync(eventoId);
            if (evento == null)
            {
                return NotFound();
            }

            var mensagens = await _context.MensagemChats
                .Include(m => m.Remetente)
                .Include(m => m.Destinatario)
                .Where(m => m.EventoId == eventoId)
                .OrderBy(m => m.DataEnvio)
                .ToListAsync();

            ViewBag.EventoId = eventoId;
            ViewBag.NomeEvento = evento.NomeEvento;
            ViewBag.CurrentUserId = user.Id;

            return View(mensagens);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnviarMensagem(int eventoId, int remetenteId, int destinatarioId, string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
            {
                return BadRequest();
            }

            var evento = await _context.Eventos.FindAsync(eventoId);
            if (evento == null)
            {
                return NotFound();
            }

            var mensagemChat = new MensagemChat
            {
                EventoId = eventoId,
                RemetenteId = remetenteId,
                DestinatarioId = destinatarioId,
                TipoDestinatario = "Convidado",
                Conteudo = conteudo,
                DataEnvio = DateTime.Now,
                EhRespostaAssistente = false
            };

            _context.MensagemChats.Add(mensagemChat);
            await _context.SaveChangesAsync();

            var remetente = await _context.Pessoas.FindAsync(remetenteId);
            var nomeUsuario = remetente?.Nome ?? "Usuário";

            await _hubContext.Clients.Group($"Evento_{eventoId}")
                .SendAsync("ReceiveMessage", remetenteId, conteudo, nomeUsuario, DateTime.Now);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ListarConversas()
        {
            var eventos = await _context.Eventos
                .Select(e => new
                {
                    e.Id,
                    e.NomeEvento,
                    MensagensNaoLidas = _context.MensagemChats.Count(m => m.EventoId == e.Id)
                })
                .ToListAsync();

            return View(eventos);
        }
    }
}
