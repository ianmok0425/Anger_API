using System.Threading.Tasks;

namespace Anger_API.Database.Settings
{
    public interface ISettingRepository : IRepository
    {
        Task<Setting> Retrieve();
    }
}