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

                if (string.IsNullOrWhiteSpace(c.ApiKey))
                {
                    throw new Exception("Missing ApiKey in Config.json.");
                }
            }
            return c;
        }
    }
}