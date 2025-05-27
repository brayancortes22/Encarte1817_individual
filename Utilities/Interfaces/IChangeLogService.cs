using System.Threading.Tasks;

namespace Utilities.Helpers
{
    public interface IChangeLogService
    {
        Task LogChange<T>(T oldEntity, T newEntity, string action, int entityId, string tableName) where T : class;
    }
}
