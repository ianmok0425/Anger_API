using Anger_API.Database.Settings;
using Anger_Library;

namespace Anger_API.API.Models.Settings
{
    public class GetSettingRequest : APIRequest
    {
    }
    public class GetSettingResponse : ResponseBase
    {
        public Setting Setting { get; set; }
    }
}