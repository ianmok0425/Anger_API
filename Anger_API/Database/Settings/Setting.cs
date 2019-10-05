using System;


namespace Anger_API.Database.Settings
{
    public class Setting : Table
    {
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public bool Close { get; set; }
        public string CloseMessage { get; set; }
        public string LatestAppVersion { get; set; }
    }
}