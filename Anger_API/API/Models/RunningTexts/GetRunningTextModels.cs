using System.Collections.Generic;

using Anger_API.Database.RunningTexts;
using Anger_Library;

namespace Anger_API.API.Models.RunningTexts
{
    public class GetRunningTextRequest: APIRequest
    {
    }
    public class GetRunningTextResponse: ResponseBase
    {
        public List<RunningText> RunningTexts { get; set; }
    }
}