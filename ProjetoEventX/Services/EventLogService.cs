using ProjetoEventX.Data;
using ProjetoEventX.Models;

namespace ProjetoEventX.Services
{
    public class EventLogService
    {
        private readonly EventXContext _context;

        public EventLogService(EventXContext context)
        {
            _context = context;
        }

        public async Task LogAsync(int eventId, int? userId, string actionType, string description)
        {
            var log = new EventLog
            {
                EventId = eventId,
                UserId = userId,
                ActionType = actionType,
                Description = description,
                CreatedAt = DateTime.UtcNow
            };

            _context.EventLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
