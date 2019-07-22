using System;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;
using Anger_API.API.Models.Abstract;

namespace Anger_API.API.Controllers.Tests
{
    using Database.Tests;
    using Models.Tests;

    [ApiKeyAuthorize]
    public class TestController : AngerApiController
    {
        public ITestRepository TestRepo { get; }
        public TestController(
            ITestRepository testRepo,
            IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            TestRepo = testRepo ?? throw new ArgumentNullException(nameof(TestRepo));
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
            model.Validate();
            var test = TestRepo.RetrieveByID<Test>(new Test(), 5); 
            var rsp = new TestResponse() { Message = "test", Test = test };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}