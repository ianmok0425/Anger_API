﻿using System;
using System.Threading.Tasks;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;
using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.PreMembers;

using Anger_API.Database.Members;
using Anger_API.Database.PreMembers;
using static Anger_API.Database.Members.Member;

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
        [Route("api/preMember/reg")]
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
                Password = model.Password,
                Status = (int)PreMemberStatus.Active
            };
            APIReturnCode apiReturnCode = MemberRepo.VerifyNewMember(preMember);
            if (apiReturnCode == APIReturnCode.Success)
            {
                string preMemberID = await PreMemberRepo.CreateAndSendVerifyCode(preMember);
                var rsp = new RegPrememberResponse() { PreMemberID = preMemberID }; // to be continue should not be static
                return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
            }
            else
                return ResultFactory.CreateResult(ReturnCode.Error500, apiReturnCode);
        }
        [HttpPost]
        [Route("api/preMember/verify")]
        public async Task<AngerResult> VerifyPreMember([FromBody]VerifyPreMemberRequest model)
        {
            if (model == null) throw new NullReferenceException();
            model.Validate();

            long preMemberID = Convert.ToInt64(model.PreMemberID);
            PreMember preMember = await PreMemberRepo.RetrieveByID<PreMember>(preMemberID);

            // Verify PreMember
            if (preMember == null)
                return ResultFactory.CreateResult(ReturnCode.Error500, APIReturnCode.PreMemberNotExist);
            else if (preMember.Verified == true)
                return ResultFactory.CreateResult(ReturnCode.Error500, APIReturnCode.PreMemberHasBeenVerified);
            else if (preMember.VerifyCode != model.Code)
                return ResultFactory.CreateResult(ReturnCode.Error500, APIReturnCode.InvalidVerifyCode);
            await PreMemberRepo.Verified(preMemberID, preMember);

            // Reg As Member
            Member member = new Member()
            {
                Name = preMember.Name,
                Mobile = preMember.Mobile,
                Email = preMember.Email,
                Account = preMember.Account,
                Password = preMember.Password,
                Status = (MemberStatus)preMember.Status
            };
            await MemberRepo.CreateAsync(member);
            var m = await MemberRepo.RetrieveMemberByAcPw(member.Account, member.Password);
            var rsp = new VerifyPreMemberReponse() { Member = m };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }

        [HttpPost]
        [Route("api/preMember/resendVerifyCode")]
        public async Task<AngerResult> ResendVerifyCode([FromBody]ResendVerifyCodeRquest model)
        {
            if (model == null) throw new NullReferenceException();
            model.Validate();
            long preMemberID = Convert.ToInt64(model.PreMemberID);
            PreMember preMember = await PreMemberRepo.RetrieveByID<PreMember>(preMemberID);

            if (preMember == null)
                return ResultFactory.CreateResult(ReturnCode.Error500, APIReturnCode.PreMemberNotExist);

            await PreMemberRepo.ResendVerifyCode(preMemberID, preMember);
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success);
        }
    }
}