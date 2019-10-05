using System;
using System.Threading.Tasks;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;

using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.Notices;

namespace Anger_API.API.Controllers.Notices
{
    using Database.Notices;
    [ApiKeyAuthorize]
    public class NoticeController : AngerApiController
    {
        public INoticeRepository NoticeRepo { get; }
        public NoticeController(
           INoticeRepository noticeRepo,
           IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            NoticeRepo = noticeRepo ?? throw new ArgumentNullException(nameof(NoticeRepo));
        }

        [Route("api/notice/get")]
        [HttpGet]
        public async Task<AngerResult> GetFavPost([FromUri] GetNoticeRequest model)
        {
            var notices = await NoticeRepo.Retrieve();
            var rsp = new GetNoticeResponse() { Notices = notices };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}