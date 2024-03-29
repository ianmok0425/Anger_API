﻿using System;
using System.Web.Http;
using System.Threading.Tasks;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;
using Anger_API.API.Models.Abstract;
using Anger_API.Library.MailService;

namespace Anger_API.API.Controllers.Tests
{
    using Database.Tests;
    using Models.Tests;

    [ApiKeyAuthorize]
    public class TestController : AngerApiController
    {
        public IMailService MailService { get; }
        public ITestRepository TestRepo { get; }
        public TestController(
            IMailService mailService,
            ITestRepository testRepo,
            IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            MailService = mailService ?? throw new ArgumentNullException(nameof(MailService));
            TestRepo = testRepo ?? throw new ArgumentNullException(nameof(TestRepo));
        }
        [HttpGet]
        public TestResponse Get([FromUri]int id)
        {
            return new TestResponse() { Message = "Get" };
        }
        [HttpPost]
        public async Task<AngerResult> Post([FromBody] TestRequest model)
        {
            if (model == null) throw new NullReferenceException();
            model.Validate();
            Test t = new Test()
            {
                Content = "32593289432fdsfew"
            };
            await TestRepo.CreateAsync(t);
            var test = await TestRepo.RetrieveByID<Test>(6);
            MailService.SendVerifyEmail("123423", "莫先生", "ianmok@itopia.com.hk");
            var rsp = new TestResponse() { Message = "test", Test = test };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}