using System;
using System.Threading.Tasks;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;

using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.FavPosts;

using Anger_API.Database.Views.FavPost;

namespace Anger_API.API.Controllers.FavPosts
{
    [ApiKeyAuthorize]
    public class FavPostController : AngerApiController
    {
        public IFavPostRepository FavPostRepo { get; }
        public FavPostController(
           IFavPostRepository favPostRepo,
           IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            FavPostRepo = favPostRepo ?? throw new ArgumentNullException(nameof(FavPostRepo));
        }

        [Route("api/favpost/get")]
        [HttpGet]
        public async Task<AngerResult> GetFavPost([FromUri] GetFavPostRequest model)
        {
            if (model == null) throw new NullReferenceException();

            var favPosts = await FavPostRepo.RetrieveFavPostList(model.MemberID, model.StartRowNo);

            var rsp = new GetFavPostResponse() { FavPosts = favPosts };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}