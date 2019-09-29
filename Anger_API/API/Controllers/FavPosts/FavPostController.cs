﻿using System;
using System.Threading.Tasks;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;

using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.FavPosts;

using Anger_API.Database.Views.FavPost;
using Anger_API.Database.FavPosts;

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

            await FavPostRepo.CreateAsync(favPost);

            var rsp = new AddFavPostResponse() { };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}