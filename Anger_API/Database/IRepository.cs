using System.Threading.Tasks;

namespace Anger_API.Database
{
    public interface IRepository
    {
        Task<string> CreateAsync(Table table);
        Task<T> RetrieveByID<T>(long ID);
    }
}