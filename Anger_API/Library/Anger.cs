using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Anger_API.Library
{
    public static class Anger
    {
        public class Config
        {
            public string ApiKey { get; set; }
            public string ConnectionString { get; set; }
            public Mail Mail { get; set; }
        }
        public class Mail
        {
            public string SmtpAddress { get; set; }
            public int? Port { get; set; }
            public string Address { get; set; }
            public string Account { get; set; }
            public string Password { get; set; }
        }
        public static Config GetConfig()
        {
            var path = System.Web.HttpContext.Current.Request.MapPath("~\\json\\Config.json");
            var c = new Config();
            var o1 = JObject.Parse(File.ReadAllText(path));

            // read JSON directly from a file
            using (StreamReader file = File.OpenText(path))
            using (var reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                JsonSerializer serializer = new JsonSerializer();
                c = (Config)serializer.Deserialize(new JTokenReader(o2), typeof(Config));

                if (string.IsNullOrWhiteSpace(c.ApiKey) ||
                    string.IsNullOrWhiteSpace(c.ConnectionString))
                {
                    throw new Exception("Missing Information in Config.json.");
                }
                if (string.IsNullOrWhiteSpace(c.Mail.SmtpAddress) ||
                    c.Mail.Port == null ||
                    string.IsNullOrWhiteSpace(c.Mail.Address) ||
                    string.IsNullOrWhiteSpace(c.Mail.Account) ||
                    string.IsNullOrWhiteSpace(c.Mail.Password))
                {
                    throw new Exception("Missing Mail Information in Config.json.");
                }
            }
            return c;
        }
    }
}