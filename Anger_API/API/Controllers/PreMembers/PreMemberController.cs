using System;
using System.Threading.Tasks;
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
        public IPreMemberRepository PreMemberRepo { get; }
        public IMemberRepository MemberRepo { get; }
        public PreMemberController(
            IPreMemberRepository preMemberRepo,
            IMemberRepository memberRepo,
           IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            PreMemberRepo = preMemberRepo ?? throw new ArgumentNullException(nameof(PreMemberRepo));
            MemberRepo = memberRepo ?? throw new ArgumentNullException(nameof(MemberRepo));
        }
        [HttpPost]
        [Route("api/preMember/Reg")]
        public async Task<AngerResult> RegPreMember([FromBody] RegPreMemberRequest model)
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
                await PreMemberRepo.CreateAndSendVerifyCode(preMember);
                var rsp = new RegPrememberResponse() { PreMemberID = "9" }; // to be continue should not be static
                return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
            }
            else
                return ResultFactory.CreateResult(ReturnCode.Error500, apiReturnCode);
        }
    }
}