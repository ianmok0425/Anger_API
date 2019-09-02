using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Anger_API.Database.RunningTexts
{
    public interface IRunningTextRepository : IRepository
    {
        Task<List<RunningText>> RetrieveAll(DateTime? createdAt);
        Task<List<RunningText>> RetrieveApprovedList(DateTime? createdAt);
        Task<List<RunningText>> RetrieveNotApprovedList(DateTime? createdAt);
    }
}