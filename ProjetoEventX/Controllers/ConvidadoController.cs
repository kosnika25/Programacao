using Microsoft.AspNetCore.Mvc;
using ProjetoEventX.Models;
using ProjetoEventX.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEventX.Controllers
{
    public class ConvidadoController : Controller
    {
        private readonly EventXContext _context;
        private readonly UserManager<Convidado> _userManager;
        private readonly SignInManager<Convidado> _signInManager;

        public ConvidadoController(EventXContext context, UserManager<Convidado> userManager, SignInManager<Convidado> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Cadastro de Convidado
        [HttpGet]
        public IActionResult Cadastro(int eventoId)
        {
            ViewBag.EventoId = eventoId;
            return View();
        }

        // POST: Cadastro de Convidado
        [HttpPost]
        public async Task<IActionResult> Cadastro(ConvidadoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var convidado = new Convidado
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Pessoa = new Pessoa {
                        Nome = model.Nome,
                        Email = model.Email,
                        Telefone = model.Telefone,
                        Endereco = "",
                        Cpf = "00000000000"
                    },
                    EmailConfirmed = false
                };
                var senhaTemp = GenerateTemporaryPassword();
                var result = await _userManager.CreateAsync(convidado, senhaTemp);
                if (result.Succeeded)
                {
                    var lista = new ListaConvidado
                    {
                        ConvidadoId = convidado.Id,
                        EventoId = model.EventoId,
                        ConfirmaPresenca = "Pendente"
                    };
                    _context.ListasConvidados.Add(lista);
                    await _context.SaveChangesAsync();
                    // TODO: Enviar email com senha ou link de ativação
                    return RedirectToAction("Listagem", new { eventoId = model.EventoId });
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            ViewBag.EventoId = model.EventoId;
            return View(model);
        }

        // GET: Listagem de Convidados
        [HttpGet]
        public IActionResult Listagem(int eventoId)
        {
            var convidados = _context.ListasConvidados
                .Where(l => l.EventoId == eventoId)
                .Select(l => l.Convidado)
                .ToList();
            ViewBag.EventoId = eventoId;
            return View(convidados);
        }

        // POST: Reenviar acesso
        [HttpPost]
        public async Task<IActionResult> ReenviarAcesso(int convidadoId)
        {
            var convidado = await _userManager.FindByIdAsync(convidadoId.ToString());
            if (convidado != null)
            {
                var senhaTemp = GenerateTemporaryPassword();
                await _userManager.RemovePasswordAsync(convidado);
                await _userManager.AddPasswordAsync(convidado, senhaTemp);
                // TODO: Enviar email com nova senha ou link
            }
            return RedirectToAction("Listagem", new { eventoId = _context.ListasConvidados.FirstOrDefault(l => l.ConvidadoId == convidadoId)?.EventoId });
        }

        private string GenerateTemporaryPassword()
        {
            return "EvX" + Guid.NewGuid().ToString("N").Substring(0, 8) + "!";
        }
    }
}
