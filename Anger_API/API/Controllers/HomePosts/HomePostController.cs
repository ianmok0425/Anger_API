using System;
using System.Threading.Tasks;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;

using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.HomePosts;

using Anger_API.Database.Views.HomePost;

namespace Anger_API.API.Controllers.HomePosts
{
    [ApiKeyAuthorize]
    public class HomePostController : AngerApiController
    {
        public IHomePostRepository HomePostRepo { get; }
        public HomePostController(
           IHomePostRepository homePostRepo,
           IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            HomePostRepo = homePostRepo ?? throw new ArgumentNullException(nameof(HomePostRepo));
        }

        [Route("api/homepost/get")]
        [HttpGet]
        public async Task<AngerResult> GetHomePost([FromUri] GetHomePostRequest model)
        {
            if (model == null) throw new NullReferenceException();

            var homePosts = await HomePostRepo.RetrieveHomePostList(model.StartRowNo);

            var rsp = new GetHomePostResponse() { HomePosts = homePosts };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}