using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoEventX.Data;
using ProjetoEventX.Models;

namespace ProjetoEventX.Controllers
{
    [Authorize]
    public class EventLogController : Controller
    {
        private readonly EventXContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventLogController(EventXContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: EventLog/Historico?eventoId=5
        public async Task<IActionResult> Historico(int eventoId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Forbid();

            var evento = await _context.Eventos.FirstOrDefaultAsync(e => e.Id == eventoId);
            if (evento == null) return NotFound();

            // Apenas o organizador do evento pode ver o histórico
            if (evento.OrganizadorId != user.Id)
                return Forbid();

            var logs = await _context.EventLogs
                .Where(l => l.EventId == eventoId)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();

            ViewBag.Evento = evento;
            return View(logs);
        }
    }
}
