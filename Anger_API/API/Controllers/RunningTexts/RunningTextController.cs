using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;
using Anger_API.API.Models.Abstract;

using Anger_API.API.Models.RunningTexts;
using Anger_API.Database.RunningTexts;
using Anger_API.Database.Members;

namespace Anger_API.API.Controllers.RunningTexts
{
    [ApiKeyAuthorize]
    public class RunningTextController : AngerApiController
    {
        public IRunningTextRepository RunningTextRepo { get; }
        public IMemberRepository MemberRepo { get; }
        public RunningTextController(
            IRunningTextRepository runningTextRepo,
            IMemberRepository memberRepo,
           IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            RunningTextRepo = runningTextRepo ?? throw new ArgumentNullException(nameof(RunningTextRepo));
            MemberRepo = memberRepo ?? throw new ArgumentNullException(nameof(MemberRepo));
        }
        [HttpPost]
        [Route("api/runningText/upload")]
        public async Task<AngerResult> UploadRunningText([FromBody] UploadRunningTextRequest model)
        {
            if (model == null) throw new NullReferenceException();
            model.Validate();

            long memberID = Convert.ToInt64(model.MemberID);

            var member = await MemberRepo.RetrieveByID<Member>(memberID);

            if(member == null)
                return ResultFactory.CreateResult(ReturnCode.Error500, APIReturnCode.MemberNotExist);
            else
            {
                RunningText rt = new RunningText()
                {
                    Content = model.Content,
                    MemberID = memberID
                };
                await RunningTextRepo.CreateAsync(rt);
                return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success);
            }
        }
        [HttpGet]
        [Route("api/runningText/get")]
        public async Task<AngerResult> GetRunningText([FromUri] GetRunningTextRequest model)
        {
            List<RunningText> rts = await RunningTextRepo.RetrieveTodayList();
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, new GetRunningTextResponse() { RunningTexts = rts });
        }
    }
}