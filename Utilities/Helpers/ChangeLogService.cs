using Entity.Model;
using Entity.Context;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;

namespace Utilities.Helpers
{
    public class ChangeLogService : IChangeLogService
    {
        private readonly LogDbContext _logContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChangeLogService(LogDbContext logContext, IHttpContextAccessor httpContextAccessor)
        {
            _logContext = logContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LogChange<T>(T oldEntity, T newEntity, string action, int entityId, string tableName) 
            where T : class
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Sistema";

            var changeLog = new ChangeLog
            {
                UserName = userName,
                IdTable = entityId,
                TableName = tableName,
                OldValues = oldEntity != null ? JsonSerializer.Serialize(oldEntity) : null,
                NewValues = newEntity != null ? JsonSerializer.Serialize(newEntity) : null,
                Action = action,
                Active = "1",
                CreateAt = DateTime.UtcNow
            };

            await _logContext.ChangeLogs.AddAsync(changeLog);
            await _logContext.SaveChangesAsync();
        }
    }
}
