using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anger_API.Database.RunningTexts
{
    public class RunningTextRepository : Repository, IRunningTextRepository
    {
        public override string TableName => "Anger_RunningText";
    }
}