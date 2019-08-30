using System.Threading.Tasks;

namespace Anger_API.Database.Admins
{
    public interface IAdminRepository : IRepository
    {
        Task<bool> VerfiyAdmin(string account, string password);
    }
}