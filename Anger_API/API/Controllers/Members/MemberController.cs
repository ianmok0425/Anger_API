using System;
using System.Threading.Tasks;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;
using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.Members;
using Anger_API.Database.Members;

namespace Anger_API.API.Controllers.Members
{
    [ApiKeyAuthorize]
    public class MemberController : AngerApiController
    {
        public IMemberRepository MemberRepo { get; }
        public MemberController(
            IMemberRepository memberRepo,
           IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            MemberRepo = memberRepo ?? throw new ArgumentNullException(nameof(MemberRepo));
        }

        [HttpPost]
        [Route("api/member/login")]
        public async Task<AngerResult> RegPreMember([FromBody] LoginRequest model)
        {
            if (model == null) throw new NullReferenceException();
            model.Validate();

            Member member = await MemberRepo.RetrieveByAC(model.Account);
            if(member == null)
                return ResultFactory.CreateResult(ReturnCode.Error500, APIReturnCode.InvalidAccount);

            var m = await MemberRepo.RetrieveMemberByAcPw(model.Account, model.Password);
            if(m == null)
                return ResultFactory.CreateResult(ReturnCode.Error500, APIReturnCode.InvalidPassword);

            var rsp = new LoginResponse() { Member = m };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}