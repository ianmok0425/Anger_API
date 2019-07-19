using System;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;
using Anger_API.API.Models.Abstract;

namespace Anger_API.API.Controllers.Tests
{
    using Anger_API.Database.Logs;
    using Models.Tests;

    [ApiKeyAuthorize]
    public class TestController : AngerApiController
    {
        public ILogRepository LogRepo { get; }
        public TestController(
            IResultFactory<AngerResult> resultFactory,
            ILogRepository logRepo) : base(resultFactory)
        {
            LogRepo = logRepo ?? throw new ArgumentNullException(nameof(LogRepo));
        }
        [HttpGet]
        public TestResponse Get([FromUri]int id)
        {
            return new TestResponse() { Message = "Get" };
        }
        [HttpPost]
        public AngerResult Post([FromBody] TestRequest model)
        {
            if (model == null) throw new NullReferenceException();
            var rsp = new TestResponse() { Message = "test" };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}