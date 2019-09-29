using System;
using System.Threading.Tasks;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;

using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.FavPosts;

namespace Anger_API.API.Controllers.FavPosts
{
    [ApiKeyAuthorize]
    public class FavPostController : AngerApiController
    {
        public Database.FavPosts.IFavPostRepository FavPostRepo { get; }
        public Database.Views.FavPost.IFavPostRepository VE_FavPostRepo { get; }
        public FavPostController(
           Database.FavPosts.IFavPostRepository favPostRepo,
           Database.Views.FavPost.IFavPostRepository ve_FavPostRepo,
           IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            FavPostRepo = favPostRepo ?? throw new ArgumentNullException(nameof(FavPostRepo));
            VE_FavPostRepo = ve_FavPostRepo ?? throw new ArgumentNullException(nameof(VE_FavPostRepo));
        }

        [Route("api/favpost/get")]
        [HttpGet]
        public async Task<AngerResult> GetFavPost([FromUri] GetFavPostRequest model)
        {
            if (model == null) throw new NullReferenceException();

            var favPosts = await VE_FavPostRepo.RetrieveFavPostList(model.MemberID, model.StartRowNo);

            var rsp = new GetFavPostResponse() { FavPosts = favPosts };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
        [Route("api/favpost/amend")]
        [HttpPost]
        public async Task<AngerResult> AmendFavPost([FromBody] AmendFavPostRequest model)
        {
            if (model == null) throw new NullReferenceException();
            model.Validate();

            var favPost = new Database.FavPosts.FavPost();

            string favPostID = "";

            if (model.ActionVal == 1)
            {
                favPost.MemberID = model.MemberIDVal;
                favPost.PostID = model.PostIDVal;
                favPostID = await FavPostRepo.CreateAsync(favPost);
            }
            else if (model.ActionVal == 2)
            {
                favPost.ID = model.IDVal.ToString();
                await FavPostRepo.DeleteAsync(favPost);
            }

            var rsp = new AmendFavPostResponse() { FavPostID = favPostID };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}