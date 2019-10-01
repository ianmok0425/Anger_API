using System;
using System.Collections.Generic;
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
            model.Validate();

            var favPosts = new List<Database.Views.FavPost.FavPost>();
            if (model.ActionVal == (int)GetFavPostAction.GetFavPostList)
                favPosts = await VE_FavPostRepo.RetrieveFavPostList(model.MemberIDVal, model.StartRowNoVal);
            else if (model.ActionVal == (int)GetFavPostAction.GetFavPostByPostIDAndMemberID)
            {
                var fp = await VE_FavPostRepo.RetrieveFavPostListByPostIDAndMemberID(model.MemberIDVal, model.PostIDVal);

                if(fp == null) return ResultFactory.CreateResult(ReturnCode.Error500, APIReturnCode.FavPostNotExist);

                favPosts.Add(fp);
            }
            var rsp = new GetFavPostResponse() { FavPosts = favPosts };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
        [Route("api/favpost/add")]
        [HttpPost]
        public async Task<AngerResult> AddFavPost([FromBody] AddFavPostRequest model)
        {
            if (model == null) throw new NullReferenceException();
            model.Validate();

            var favPost = new Database.FavPosts.FavPost()
            {
                MemberID = model.MemberIDVal,
                PostID = model.PostIDVal
            }; 
            string favPostID = await FavPostRepo.CreateAsync(favPost);

            var rsp = new AmendFavPostResponse() { FavPostID = favPostID };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
        [Route("api/favpost/delete")]
        [HttpDelete]
        public async Task<AngerResult> DeleteFavPost([FromBody] DeleteFavPostRequest model)
        {
            if (model == null) throw new NullReferenceException();
            model.Validate();

            var favPost = new Database.FavPosts.FavPost()
            {
                ID = model.FavPostID
            };
            await FavPostRepo.DeleteAsync(favPost);

            var rsp = new DeleteFavPostResponse() { };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}