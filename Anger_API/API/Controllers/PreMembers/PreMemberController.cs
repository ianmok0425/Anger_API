using System;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;
using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.PreMembers;

using Anger_API.Database.Members;
using Anger_API.Database.PreMembers;

namespace Anger_API.API.Controllers.PreMembers
{
    [ApiKeyAuthorize]
    public class PreMemberController : AngerApiController
    {
        public IMemberRepository MemberRepo { get; }
        public PreMemberController(
            IMemberRepository memberRepo,
           IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            MemberRepo = memberRepo ?? throw new ArgumentNullException(nameof(MemberRepo));
        }
        [HttpPost]
        [Route("api/preMember/Reg")]
        public AngerResult RegPreMember([FromBody] RegPreMemberRequest model)
        {
            if (model == null) throw new NullReferenceException();
            model.Validate();
            PreMember preMember = new PreMember()
            {
                Account = model.Account,
                Email = model.Email,
                Mobile = model.Mobile,
                Name = model.Name,
                Password = model.Password
            };
            APIReturnCode apiReturnCode = MemberRepo.VerifyNewMember(preMember);
            if (apiReturnCode == APIReturnCode.Success)
            {
                var rsp = new RegPrememberResponse() { };
                return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
            }
            else
                return ResultFactory.CreateResult(ReturnCode.Error500, apiReturnCode);
        }
    }
}