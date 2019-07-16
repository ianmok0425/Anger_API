using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;
using Anger_API.API.Models.Abstract;

namespace Anger_API.API.Controllers.Tests
{
    using Anger_Library;
    using Models.Tests;
    [ApiKeyAuthorize]
    public class TestController : AngerApiController
    {
        public TestController(IResultFactory<AngerResult> resultFactory) : base(resultFactory) { }
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