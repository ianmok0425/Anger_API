using System.Threading.Tasks;

namespace Anger_API.Database
{
    public interface IRepository
    {
        Task<string> CreateAsync(Table table);
        Task DeleteAsync(Table table);
        Task<T> RetrieveByID<T>(long ID);
        Task Update(long ID, Table table);
    }
}