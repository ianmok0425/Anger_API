using System;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;
using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.PreMembers;

namespace Anger_API.API.Controllers.PreMembers
{
    [ApiKeyAuthorize]
    public class PreMemberController : AngerApiController
    {
        public PreMemberController(
           IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
        }
        [HttpPost]
        [Route("api/preMember/Reg")]
        public AngerResult RegPreMember([FromBody] RegPreMemberRequest model)
        {
            if (model == null) throw new NullReferenceException();
            model.Validate();
            var rsp = new RegPrememberResponse() { };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}