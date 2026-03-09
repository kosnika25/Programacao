using ProjetoEventX.Data;
using ProjetoEventX.Models;

namespace ProjetoEventX.Services
{
    public class NotificationService
    {
        private readonly EventXContext _context;

        public NotificationService(EventXContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(int userId, string title, string message, string type, string? link = null)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                Type = type,
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                Link = link
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task CreateForManyAsync(IEnumerable<int> userIds, string title, string message, string type, string? link = null)
        {
            foreach (var userId in userIds.Distinct())
            {
                _context.Notifications.Add(new Notification
                {
                    UserId = userId,
                    Title = title,
                    Message = message,
                    Type = type,
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow,
                    Link = link
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
