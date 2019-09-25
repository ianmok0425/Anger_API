using System;
using System.Threading.Tasks;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;

using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.SearchPosts;

using Anger_API.Database.Views.SearchPost;

namespace Anger_API.API.Controllers.SearchPosts
{
    [ApiKeyAuthorize]
    public class SearchPostController : AngerApiController
    {
        public ISearchPostRepository SearchPostRepo { get; }
        public SearchPostController(
           ISearchPostRepository searchPostRepo,
           IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            SearchPostRepo = searchPostRepo ?? throw new ArgumentNullException(nameof(SearchPostRepo));
        }
        [Route("api/searchpost/get")]
        [HttpGet]
        public async Task<AngerResult> GetSearchPost([FromUri] GetSearchPostRequest model)
        {
            if (model == null) throw new NullReferenceException();
            var searchPosts = await SearchPostRepo.RetrieveSearchPostList(model.SearchText ,model.StartRowNo, model.EndRowNo);

            var rsp = new GetSearchPostResponse() { SearchPosts = searchPosts };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}