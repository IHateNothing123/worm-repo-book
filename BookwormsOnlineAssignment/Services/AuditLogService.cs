using BookwormsOnlineAssignment.Models;

namespace BookwormsOnlineAssignment.Services
{
    public class AuditLogService
    {
        private readonly AuthDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditLogService(AuthDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public void LogActivity(string userId, string action, string additionalInfo = null)
        {
            var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();

            var logEntry = new AuditLog
            {
                UserId = userId,
                Action = action,
                Timestamp = DateTime.UtcNow,
                AdditionalInfo = additionalInfo
            };

            _context.AuditLogs.Add(logEntry);
            _context.SaveChanges();
        }

    }
}
