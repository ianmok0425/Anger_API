using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using OfficeOpenXml;

namespace Anger_API.Service.Admin.RunningText
{
    public interface IRunningTextService
    {
        Task<ExcelPackage> GenerateList(DateTime? createdOn);
    }
}