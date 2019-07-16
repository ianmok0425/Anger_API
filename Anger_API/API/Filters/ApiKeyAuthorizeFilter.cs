using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.IO;

using Newtonsoft.Json.Linq;

using Autofac.Integration.WebApi;

using Anger_Library;
using Anger_API.API.Attributes;
using Newtonsoft.Json;
using static Anger_Library.Anger;

namespace Anger_API.API.Filters
{
    public class ApiKeyAuthorizeFilter : IAutofacAuthenticationFilter
    {
        private const string _Header_ApiKey = "anger-api-key";

        public ApiKeyAuthorizeFilter() { }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var attr = context.ActionContext.ActionDescriptor.GetCustomAttributes<ApiKeyAuthorize>()
                .Concat(context.ActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<ApiKeyAuthorize>())
                .FirstOrDefault();

            //  No attribute for authentication
            if (attr == null) return Task.CompletedTask;

            if (context.Request.Headers.TryGetValues(_Header_ApiKey, out IEnumerable<string> validHeaders))
            {
                string apiKey = ReadKey();
                var header = validHeaders.FirstOrDefault().ToUpper();
                if (header.ToLower() == apiKey)
                {
                    return Task.CompletedTask;
                }
                else
                {
                    throw new APIInvalidKey();
                }
            }

            throw new Exception("Missing anger-api-key in request header.");
        }
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public string ReadKey()
        {
            try
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
                return c.ApiKey;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}