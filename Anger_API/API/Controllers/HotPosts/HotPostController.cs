using System;
using System.Threading.Tasks;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;

using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.HotPosts;

using Anger_API.Database.Views.HotPost;

namespace Anger_API.API.Controllers.HotPosts
{
    [ApiKeyAuthorize]
    public class HotPostController : AngerApiController
    {
        public IHotPostRepository HotPostRepo { get; }
        public HotPostController(
           IHotPostRepository hotPostRepo,
           IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            HotPostRepo = hotPostRepo ?? throw new ArgumentNullException(nameof(HotPostRepo));
        }

        [Route("api/hotpost/get")]
        [HttpGet]
        public async Task<AngerResult> GetHotPost([FromUri] GetHotPostRquest model)
        {
            if (model == null) throw new NullReferenceException();

            var hotPosts = await HotPostRepo.RetrieveHotPostList(model.StartRowNo);

            var rsp = new GetHotPostResponse() { HotPosts = hotPosts };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}