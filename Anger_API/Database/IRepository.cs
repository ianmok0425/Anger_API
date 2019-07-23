using System.Threading.Tasks;

namespace Anger_API.Database
{
    public interface IRepository
    {
        Task Create(Table table);
        Task<T> RetrieveByID<T>(long ID);
    }
}