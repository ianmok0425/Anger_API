using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anger_API.Database.Notices
{
    public interface INoticeRepository : IRepository
    {
        Task<List<Notice>> Retrieve();
    }
}