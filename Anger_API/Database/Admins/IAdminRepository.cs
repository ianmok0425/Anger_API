using System.Threading.Tasks;

namespace Anger_API.Database.Admins
{
    public interface IAdminRepository : IRepository
    {
        Task<Admin> GetAdminByAcAndPw(string account, string password);
    }
}