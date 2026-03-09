using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoEventX.Data;
using ProjetoEventX.Models;

namespace ProjetoEventX.Controllers
{
    [Authorize]
    public class NotificacaoController : Controller
    {
        private readonly EventXContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificacaoController(EventXContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Notificacao/Index
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Forbid();

            var notifications = await _context.Notifications
                .Where(n => n.UserId == user.Id)
                .OrderByDescending(n => n.CreatedAt)
                .Take(100)
                .ToListAsync();

            ViewBag.UnreadCount = notifications.Count(n => !n.IsRead);

            return View(notifications);
        }

        // GET: Notificacao/GetUnread (AJAX — chamado pelo sino)
        [HttpGet]
        public async Task<IActionResult> GetUnread()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var notifications = await _context.Notifications
                .Where(n => n.UserId == user.Id)
                .OrderByDescending(n => n.CreatedAt)
                .Take(20)
                .Select(n => new
                {
                    n.Id,
                    n.Title,
                    n.Message,
                    n.Type,
                    n.IsRead,
                    CreatedAt = n.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                    n.Link,
                    TimeAgo = GetTimeAgo(n.CreatedAt)
                })
                .ToListAsync();

            var unreadCount = await _context.Notifications
                .CountAsync(n => n.UserId == user.Id && !n.IsRead);

            return Json(new { notifications, unreadCount });
        }

        // POST: Notificacao/MarkAsRead/5
        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == user.Id);

            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }

        // POST: Notificacao/MarkAllAsRead
        [HttpPost]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var unread = await _context.Notifications
                .Where(n => n.UserId == user.Id && !n.IsRead)
                .ToListAsync();

            foreach (var n in unread)
                n.IsRead = true;

            if (unread.Any())
                await _context.SaveChangesAsync();

            return Json(new { success = true, count = unread.Count });
        }

        // GET: Notificacao/UnreadCount (lightweight AJAX)
        [HttpGet]
        public async Task<IActionResult> UnreadCount()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var count = await _context.Notifications
                .CountAsync(n => n.UserId == user.Id && !n.IsRead);

            return Json(new { count });
        }

        private static string GetTimeAgo(DateTime dateTime)
        {
            var diff = DateTime.UtcNow - dateTime;
            if (diff.TotalMinutes < 1) return "agora";
            if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes}min";
            if (diff.TotalHours < 24) return $"{(int)diff.TotalHours}h";
            if (diff.TotalDays < 7) return $"{(int)diff.TotalDays}d";
            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}
