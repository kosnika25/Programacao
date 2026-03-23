using Microsoft.AspNetCore.Mvc;
using ProjetoEventX.Models;
using ProjetoEventX.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEventX.Controllers
{
    public class FeedController : Controller
    {
        private readonly EventXContext _context;
        private readonly UserManager<Convidado> _userManager;
        private readonly UserManager<Organizador> _orgManager;

        public FeedController(EventXContext context, UserManager<Convidado> userManager, UserManager<Organizador> orgManager)
        {
            _context = context;
            _userManager = userManager;
            _orgManager = orgManager;
        }

        // GET: Feed do Evento
        [HttpGet]
        public IActionResult EventoFeed(int eventoId)
        {
            var feed = _context.PublicacoesFeed
                .Where(p => p.EventoId == eventoId)
                .OrderByDescending(p => p.Fixado)
                .ThenByDescending(p => p.DataHora)
                .ToList();
            ViewBag.EventoId = eventoId;
            return View(feed);
        }

        // POST: Comentar publicação
        [HttpPost]
        public async Task<IActionResult> Comentar(int publicacaoId, string texto)
        {
            var convidado = await _userManager.GetUserAsync(User);
            var publicacao = _context.PublicacoesFeed.FirstOrDefault(p => p.Id == publicacaoId);
            if (convidado == null || publicacao == null || string.IsNullOrWhiteSpace(texto))
                return BadRequest();
            var comentario = new ComentarioFeed
            {
                PublicacaoFeedId = publicacaoId,
                PublicacaoFeed = publicacao,
                AutorId = convidado.Id,
                Autor = convidado,
                Texto = texto,
                DataHora = DateTime.Now
            };
            _context.ComentariosFeed.Add(comentario);
            await _context.SaveChangesAsync();
            return RedirectToAction("EventoFeed", new { eventoId = publicacao.EventoId });
        }

        // POST: Excluir comentário (organizador)
        [HttpPost]
        public async Task<IActionResult> ExcluirComentario(int comentarioId)
        {
            var comentario = _context.ComentariosFeed.FirstOrDefault(c => c.Id == comentarioId);
            if (comentario == null)
                return NotFound();
            var organizador = await _orgManager.GetUserAsync(User);
            var publicacao = _context.PublicacoesFeed.FirstOrDefault(p => p.Id == comentario.PublicacaoFeedId);
            if (publicacao == null || organizador == null || publicacao.AutorId != organizador.Id)
                return Unauthorized();
            _context.ComentariosFeed.Remove(comentario);
            await _context.SaveChangesAsync();
            return RedirectToAction("EventoFeed", new { eventoId = publicacao.EventoId });
        }
    }
}
